using System;
using System.Drawing;
using System.IO;
using System.Windows;
using CressemCADViewer.Model;
using CressemCADViewer.ViewModel.Control;
using CressemDataToGraphics;
using CressemDataToGraphics.Model;
using CressemExtractLibrary;
using CressemExtractLibrary.Data;
using CressemLogger;
using CressemLogger.ViewModel;
using ImageControl.ViewModel;

namespace CressemCADViewer.ViewModel
{
	public class MainViewModel
	{
		private Window _parent = null;

		private MainViewModel() { }

		public MainViewModel(Window parent, LogControlViewModel logView)
		{
			_parent = parent;
			LogView = logView;

			GraphicsView = new GraphicsViewModel();
			PropertyView = new PropertyViewModel(_parent);
			AlarmView = new AlarmViewModel();
			LogoView = new LogoViewModel();
			Processor = new Processor();

			InitLogView();
			InitEvent();
		}

		public GraphicsViewModel GraphicsView { get; private set; }

		public PropertyViewModel PropertyView { get; private set; }

		public LogControlViewModel LogView { get; private set; }

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
			// 임시
			// inch 표시와 mm표시 구분해야함
			// 아래는 전체에 어떻게 표시할지
			// 데이터 상에는 자기 자신의 값 형태 있음
			bool useMM = true;

			var profile = ExtractLibrary.Instance.GetStepRoi(PropertyView.SelectedStepName);
			var features = ExtractLibrary.Instance.GetFeatures(
				PropertyView.SelectedStepName, PropertyView.SelectedLayerName,
				out double xDatum, out double yDatum);

			DataToGraphics dataToGraphics = new DataToGraphics(1.0f, GraphicsType.DirectX);
			GraphicsView.ClearShape();

			var proflieShapes = dataToGraphics.GetShapes(useMM, xDatum, yDatum, 0, 0, 0, false, profile);
			GraphicsView.LoadProfile(proflieShapes);

			foreach (var feature in features)
			{
				var shape = dataToGraphics.GetShapes(useMM, xDatum, yDatum, 0, 0, 0, false, feature);
				if (shape is null)
				{
					continue;
				}

				GraphicsView.AddShapes(shape);
			}
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
	}
}
