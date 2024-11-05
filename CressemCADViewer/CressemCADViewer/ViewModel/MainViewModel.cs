using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
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

			TransformMenuView.GetOrientFlip(out int orient, out bool isFlipHorizontal, out bool isRepeatAll);

			bool useMM = true;
			GraphicsView.ClearShape();
			DataToGraphics dataToGraphics = new DataToGraphics(1f, GraphicsType);

			var stepHeader = ExtractLibrary.Instance.GetStepHeader(PropertyView.SelectedStepName);

			var curProfileFeature = ExtractLibrary.Instance.GetStepProfile(PropertyView.SelectedStepName);
			if (curProfileFeature is null)
			{
				MessageBox.Show("Profile is not loaded", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			List<IGraphicsList> profileShapeList = new List<IGraphicsList>();
			var curProfileShape = dataToGraphics.GetShapes(useMM, orient, isFlipHorizontal,
				0, 0, 0, 0, 0, false, curProfileFeature);

			profileShapeList.Add(curProfileShape);

			var profileShapes = LoadFeatures(true, useMM, orient, isFlipHorizontal,
				0, 0, 0, false, stepHeader, "", ref dataToGraphics);
			profileShapeList.AddRange(profileShapes);

			profileShapeList.Add(curProfileShape);
			GraphicsView.LoadProfiles(profileShapeList);

			var curFeatures = ExtractLibrary.Instance.GetFeatures(
				PropertyView.SelectedStepName, PropertyView.SelectedLayerName);

			if (curFeatures is null)
			{
				MessageBox.Show("Feature is not loaded", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			List<IGraphicsList> featureShapeList = new List<IGraphicsList>();

			foreach (var curFeature in curFeatures)
			{
				var curFeatureShapes = dataToGraphics.GetShapes(useMM, orient, isFlipHorizontal,
					0, 0, 0, 0, 0, false, curFeature);

				featureShapeList.Add(curFeatureShapes);
			}

			//var features = LoadFeatures(isRepeatAll, useMM, orient, isFlipHorizontal,
			//	0, 0, 0, false, stepHeader, PropertyView.SelectedLayerName, ref dataToGraphics);

			//featureShapeList.AddRange(features);

			foreach (var feature in featureShapeList)
			{
				if (feature is null)
				{
					continue;
				}

				GraphicsView.AddShapes(feature);
			}

			//var features = LoadFeatures(useMM, orient, isFlipHorizontal,
			//	0, 0, 0, false, stepHeader, ref dataToGraphics);
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

		//private IEnumerable<IGraphicsList> LoadProfile(bool useMM,
		//	int globalOrient, bool isGlobalFlipHorizontal,
		//	double datumX, double datumY, int orient, bool isFlipHorizontal,
		//	IStepHeader stepHeader, ref DataToGraphics dataToGraphics)
		//{
		//	List<IGraphicsList> profileShapes = new List<IGraphicsList>();

		//	foreach (var step in stepHeader.StepRepeats)
		//	{
		//		var feature = ExtractLibrary.Instance.GetStepProfile(step.Name);
		//		var childHeader = ExtractLibrary.Instance.GetStepHeader(step.Name);

		//		PointF stepStart = new PointF((float)(step.Sx), (float)(step.Sy));
		//		stepStart = stepStart.Rotate(
		//			new PointF(0, 0), -orient, isFlipHorizontal);

		//		int childOrient = (int)((step.Angle) % 360);
		//		PointF childDatum = new PointF(
		//					(float)childHeader.XDatum, (float)childHeader.YDatum);
		//		childDatum = childDatum.Rotate(new PointF(0, 0), -orient, isFlipHorizontal);

		//		bool childIsFlipHorizontal = isFlipHorizontal ^ step.IsFlipHorizontal;

		//		for (int idxX = 0; idxX < step.Nx; idxX++)
		//		{
		//			for (int idxY = 0; idxY < step.Ny; idxY++)
		//			{
		//				System.Drawing.Point idx =
		//					new System.Drawing.Point(idxX, idxY);
		//				idx = idx.Rotate(new System.Drawing.Point(0, 0), -orient, isFlipHorizontal);

		//				double sx = datumX + stepStart.X + step.Dx * idx.X;
		//				double sy = datumY + stepStart.Y + step.Dy * idx.Y;

		//				PointF anchor = new PointF((float)sx, (float)sy);
		//				anchor = anchor.Rotate(new PointF(
		//					(float)sx, (float)sy), -(int)step.Angle, isFlipHorizontal);

		//				sx -= childDatum.X;
		//				sy -= childDatum.Y;

		//				var shape = dataToGraphics.GetShapes(useMM,
		//					globalOrient, isGlobalFlipHorizontal,
		//					sx, sy, anchor.X, anchor.Y,
		//					childOrient, isFlipHorizontal, feature);
		//				profileShapes.Add(shape);

		//				if (childHeader.StepRepeats.Any() is true)
		//				{
		//					var childShapes = LoadProfile(useMM,
		//						globalOrient, isGlobalFlipHorizontal,
		//						sx, sy, childOrient,
		//						isFlipHorizontal, childHeader, ref dataToGraphics);
		//					profileShapes.AddRange(childShapes);
		//				}
		//			}
		//		}
		//	}

		//	return profileShapes;
		//}

		private IEnumerable<IGraphicsList> LoadFeatures(bool isRepeatAll,
			bool useMM, int globalOrient, bool isGlobalFlipHorizontal,
			double datumX, double datumY, int orient, bool isFlipHorizontal,
			IStepHeader stepHeader, string layerName,
			ref DataToGraphics dataToGraphics)
		{
			List<IGraphicsList> shapes = new List<IGraphicsList>();

			foreach (var step in stepHeader.StepRepeats)
			{
				IFeatureBase[] features = null;
				if (layerName == string.Empty)
				{
					features = new IFeatureBase[] { ExtractLibrary.Instance.GetStepProfile(step.Name) };
				}
				else
				{
					features = ExtractLibrary.Instance.GetFeatures(step.Name, layerName);
				}

				var childHeader = ExtractLibrary.Instance.GetStepHeader(step.Name);

				PointF stepStart = new PointF((float)(step.Sx), (float)(step.Sy));
				stepStart = stepStart.Rotate(
					new PointF(0, 0), -orient, isFlipHorizontal);

				int childOrient = (int)((orient + step.Angle) % 360);
				bool childIsFlipHorizontal = isFlipHorizontal ^ step.IsFlipHorizontal;

				for (int idxX = 0; idxX < step.Nx; idxX++)
				{
					for (int idxY = 0; idxY < step.Ny; idxY++)
					{
						System.Drawing.Point idx =
							new System.Drawing.Point(idxX, idxY);
						idx = idx.Rotate(new System.Drawing.Point(0, 0), -orient, isFlipHorizontal);

						double sx = datumX + stepStart.X + step.Dx * idx.X;
						double sy = datumY + stepStart.Y + step.Dy * idx.Y;

						PointF anchor = new PointF((float)sx, (float)sy);
						anchor = anchor.Rotate(new PointF(
							(float)sx, (float)sy), -(int)step.Angle, isFlipHorizontal);

						sx -= childHeader.XDatum;
						sy -= childHeader.YDatum;

						foreach (var feature in features)
						{
							var shape = dataToGraphics.GetShapes(useMM,
								globalOrient, isGlobalFlipHorizontal,
								sx, sy, anchor.X, anchor.Y,
								childOrient, isFlipHorizontal, feature);
							shapes.Add(shape);
						}

						if (childHeader.StepRepeats.Any() is true)
						{
							var childShapes = LoadFeatures(isRepeatAll,
								useMM, globalOrient, isGlobalFlipHorizontal,
								sx, sy, (int)(step.Angle % 360),
								isFlipHorizontal, childHeader, layerName, ref dataToGraphics);
							shapes.AddRange(childShapes);
						}

						if (isRepeatAll is false)
						{
							break;
						}
					}

					if (isRepeatAll is false)
					{
						break;
					}
				}
			}

			return shapes;
		}
	}
}
