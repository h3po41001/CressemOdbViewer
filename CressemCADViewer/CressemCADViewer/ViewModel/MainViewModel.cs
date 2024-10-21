using System;
using System.Drawing;
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
		private MainViewModel() { }

		public MainViewModel(LogControlViewModel logView)
		{
			LogView = logView;

			GraphicsView = new GraphicsViewModel();
			PropertyView = new PropertyViewModel();
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

		private void PropertyView_LoadCamImageEvent(object sender, RoutedEventArgs e)
		{
			// 임시
			// inch 표시와 mm표시 구분해야함
			// 아래는 전체에 어떻게 표시할지
			// 데이터 상에는 자기 자신의 값 형태 있음
			bool useMM = true;

			var profile = ExtractLibrary.Instance.GetStepRoi("UNIT");		
			var features = ExtractLibrary.Instance.GetFeatures("UNIT", "L01");
			DataToGraphics dataToGraphics = new DataToGraphics(10.0f, GraphicsType.GdiPlus);

			GraphicsView.ClearShape();
			GraphicsView.LoadRoi(dataToGraphics.GetShape(useMM, profile));

			foreach (var feature in features)
			{
				GraphicsView.AddShape(dataToGraphics.GetShape(useMM, feature));
			}
		}

		private void LogoView_LogoDoubleClickedEvent(object sender, EventArgs e)
		{
			Processor.Run(DesignFormat.Odb, "D:\\Odb_Test\\21fcb008-01.tgz", "D:\\Odb_Test\\21fcb008-01");
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
