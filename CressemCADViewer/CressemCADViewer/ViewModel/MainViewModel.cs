using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using CressemCADViewer.Model;
using CressemCADViewer.ViewModel.Control;
using CressemDataToGraphics;
using CressemExtractLibrary;
using CressemExtractLibrary.Data;
using CressemExtractLibrary.Data.Interface.Features;
using CressemExtractLibrary.Data.Interface.Step;
using CressemLogger;
using CressemLogger.ViewModel;
using ImageControl.Extension;
using ImageControl.Model;
using ImageControl.Shape.Interface;
using ImageControl.ViewModel;

namespace CressemCADViewer.ViewModel
{
	public class MainViewModel
	{
		private MainViewModel() { }

		public MainViewModel(Window parent, GraphicsType graphics, LogControlViewModel logView)
		{
			Parent = parent;
			GraphicsType = graphics;
			LogView = logView;

			GraphicsView = new GraphicsViewModel(graphics);
			PropertyView = new PropertyViewModel(parent);
			TransformMenuView = new TransformMenuViewModel();
			AlarmView = new AlarmViewModel();
			LogoView = new LogoViewModel();
			Processor = new Processor();

			InitLogView();
			InitEvent();
		}

		public Window Parent { get; private set; }

		public GraphicsType GraphicsType { get; private set; }

		public GraphicsViewModel GraphicsView { get; private set; }

		public PropertyViewModel PropertyView { get; private set; }

		public LogControlViewModel LogView { get; private set; }

		public TransformMenuViewModel TransformMenuView { get; private set; }

		public AlarmViewModel AlarmView { get; private set; }

		public LogoViewModel LogoView { get; private set; }

		public Processor Processor { get; private set; }

		private void InitLogView()
		{
			CLogger.Instance.AddInfoLog("Main", "Start Main", true);
			LogView.Referesh();
		}

		private void InitEvent()
		{
			PropertyView.SelectedStepChangedEvent += PropertyView_SelectedStepChangedEvent;
			PropertyView.SelectedLayerChangedEvent += PropertyView_SelectedLayerChangedEvent;
			PropertyView.ExtractEvent += PropertyView_ExtractEvent; ;
			PropertyView.LoadCamImageEvent += PropertyView_LoadCamImageEvent;
			LogoView.LogoDoubleClickedEvent += LogoView_LogoDoubleClickedEvent;
			Processor.ProcessStarted += Processor_ProcessStarted;
			Processor.ProcessCompleted += Processor_ProcessCompleted;
		}

		private void PropertyView_SelectedStepChangedEvent(object sender, RoutedEventArgs e)
		{
			if (PropertyView.SelectedStepName is null)
			{
				return;
			}

			PropertyView.LayerNames = ExtractLibrary.Instance.GetLayerNames(
				PropertyView.SelectedStepName);
		}

		private void PropertyView_SelectedLayerChangedEvent(object sender, RoutedEventArgs e)
		{
		}

		private void PropertyView_ExtractEvent(object sender, RoutedEventArgs e)
		{
			string fileName = Path.GetFileName(PropertyView.OdbLoadPath);
			string saveFolder = Path.GetDirectoryName(PropertyView.OdbLoadPath);

			Processor.Run(DesignFormat.Odb, Path.Combine(saveFolder, fileName),
				Path.Combine(saveFolder, Path.GetFileNameWithoutExtension(fileName)));
		}

		private void PropertyView_LoadCamImageEvent(object sender, RoutedEventArgs e)
		{
			LoadCamImage();
		}

		private void LogoView_LogoDoubleClickedEvent(object sender, EventArgs e)
		{
		}

		private void Processor_ProcessStarted(object sender, bool e)
		{
			if (e is true)
			{
				AlarmView.SetState(ProcessState.Running, Color.Green);
			}
			else
			{
				AlarmView.SetState(ProcessState.Error, Color.Red);
			}
		}

		private void Processor_ProcessCompleted(object sender, bool e)
		{
			if (e is true)
			{
				AlarmView.SetState(ProcessState.Stop, Color.Green);
				PropertyView.StepNames = ExtractLibrary.Instance.GetStepNames();
			}
			else
			{
				AlarmView.SetState(ProcessState.Error, Color.Red);
			}
		}

		private void LoadCamImage()
		{
			if (PropertyView.SelectedStepName is null)
			{
				return;
			}

			if (PropertyView.SelectedLayerName is null)
			{
				return;
			}

			TransformMenuView.GetOrientFlip(out int orient, out bool isFlipHorizontal);

			bool useMM = true;
			GraphicsView.ClearShape();
			DataToGraphics dataToGraphics = new DataToGraphics(1f, GraphicsType);

			var stepHeader = ExtractLibrary.Instance.GetStepHeader(PropertyView.SelectedStepName);

			var profileShapes = LoadProfile(useMM, 0, 0, orient, isFlipHorizontal, stepHeader, ref dataToGraphics);

			List<IGraphicsList> profileShapeList = new List<IGraphicsList>(profileShapes);
			if (profileShapes.Any() is false)
			{
				MessageBox.Show("Profile is not loaded", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			var curFeature = ExtractLibrary.Instance.GetStepProfile(PropertyView.SelectedStepName);
			var curProfileShape = dataToGraphics.GetShapes(useMM, 0, 0, 0, 0, orient, isFlipHorizontal, curFeature);

			profileShapeList.Add(curProfileShape);
			GraphicsView.LoadProfiles(profileShapeList);

			//		foreach (var featrue in profile.)


			//		if (stepHeader.StepRepeats.Any() is true)
			//		{

			//		}
			//		var features = ExtractLibrary.Instance.GetFeatures(PropertyView.SelectedStepName,
			//PropertyView.SelectedLayerName);



			//foreach (var step in stepHeader.StepRepeats)
			//{
			//	var stepFeatures = ExtractLibrary.Instance.GetFeatures(step.Name, PropertyView.SelectedLayerName);

			//	foreach (var stepFeature in stepFeatures)
			//	{
			//		var shape = dataToGraphics.GetShapes(useMM, step.Sx, step.Sy, 0, 0, orient, isFlipHorizontal, stepFeature);
			//		if (shape is null)
			//		{
			//			continue;
			//		}

			//		GraphicsView.AddShapes(shape);
			//	}
			//}
		}

		private IEnumerable<IGraphicsList> LoadProfile(bool useMM,
			double datumX, double datumY, int orient, bool isFlipHorizontal,
			IStepHeader stepHeader, ref DataToGraphics dataToGraphics)
		{
			List<IGraphicsList> profileShapes = new List<IGraphicsList>();

			foreach (var step in stepHeader.StepRepeats)
			{
				var feature = ExtractLibrary.Instance.GetStepProfile(step.Name);
				var childHeader = ExtractLibrary.Instance.GetStepHeader(step.Name);

				PointF stepStart = new PointF((float)(step.Sx), (float)(step.Sy));
				stepStart = stepStart.Rotate(
					new PointF((float)(0), (float)(0)), -orient, isFlipHorizontal);

				int childOrient = (int)((step.Angle) % 360);

				for (int idxX = 0; idxX < step.Nx; idxX++)
				{
					for (int idxY = 0; idxY < step.Ny; idxY++)
					{
						int x = idxX;
						int y = idxY;
						double childXDatum = childHeader.XDatum;
						double childYDatum = childHeader.YDatum;

						if (orient == 90)
						{
							x = idxY;
							y = -idxX;
							childXDatum = childHeader.YDatum;
							childYDatum = -childHeader.XDatum;
						}

						double sx = datumX + stepStart.X + step.Dx * x;
						double sy = datumY + stepStart.Y + step.Dy * y;

						PointF anchor = new PointF((float)sx, (float)sy);
						anchor = anchor.Rotate(new PointF(
							(float)sx, (float)sy), -(int)step.Angle, isFlipHorizontal);

						sx -= childXDatum;
						sy -= childYDatum;

						var shape = dataToGraphics.GetShapes(useMM, sx, sy,
							anchor.X, anchor.Y,
							childOrient, isFlipHorizontal, feature);
						profileShapes.Add(shape);

						if (childHeader.StepRepeats.Any() is true)
						{
							var childShapes = LoadProfile(useMM, sx, sy,
								childOrient, isFlipHorizontal, childHeader, ref dataToGraphics);
							profileShapes.AddRange(childShapes);
						}
					}
				}
			}

			return profileShapes;
		}
	}
}
