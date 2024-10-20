using System;
using System.Drawing;
using System.Windows;
using CressemCADViewer.Factory;
using CressemCADViewer.Model;
using CressemCADViewer.ViewModel.Control;
using CressemExtractLibrary;
using CressemExtractLibrary.Convert;
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
			if (PropertyView.SelectedStepName is null)
			{
				return;
			}

			//if (PropertyView.SelectedLayerName is null)
			//{
			//	return;
			//}

			RectangleF stepRoi = ExtractLibrary.Instance.GetStepRoi(PropertyView.SelectedStepName);
			GraphicsView.LoadRoi(DrawingFactory.Instance.GetGdiRoi(stepRoi), 10.0f);

			GraphicsView.ClearShape();
			GraphicsView.AddShape(DrawingFactory.Instance.GetGdiLine(
				new PointF(
					(float)Converter.Instance.ConvertInchToMM(-0.10964587),
					(float)Converter.Instance.ConvertInchToMM(0.21291939)),
				new PointF(
					(float)Converter.Instance.ConvertInchToMM(-0.11013169),
					(float)Converter.Instance.ConvertInchToMM(0.21192992))));

			GraphicsView.AddShape(DrawingFactory.Instance.GetGdiArc(
				new PointF(
					(float)Converter.Instance.ConvertInchToMM(0.209586614173),
					(float)Converter.Instance.ConvertInchToMM(-0.216535433071)),
				new PointF(
					(float)Converter.Instance.ConvertInchToMM(0.209586614173),
					(float)Converter.Instance.ConvertInchToMM(-0.216535433071)),
				new PointF(
					(float)Converter.Instance.ConvertInchToMM(0.216535433071),
					(float)Converter.Instance.ConvertInchToMM(-0.216535433071))));

			GraphicsView.AddShape(DrawingFactory.Instance.GetGdiArc(
				new PointF(
					(float)Converter.Instance.ConvertInchToMM(-0.262854330709),
					(float)Converter.Instance.ConvertInchToMM(-0.216535433071)),
				new PointF(
					(float)Converter.Instance.ConvertInchToMM(-0.262854330709),
					(float)Converter.Instance.ConvertInchToMM(-0.216535433071)),
				new PointF(
					(float)Converter.Instance.ConvertInchToMM(-0.255905511811),
					(float)Converter.Instance.ConvertInchToMM(-0.216535433071))));

			GraphicsView.AddShape(DrawingFactory.Instance.GetGdiArc(
				new PointF(
					(float)Converter.Instance.ConvertInchToMM(-0.262854330709),
					(float)Converter.Instance.ConvertInchToMM(-0.196850393701)),
				new PointF(
					(float)Converter.Instance.ConvertInchToMM(-0.262854330709),
					(float)Converter.Instance.ConvertInchToMM(-0.196850393701)),
				new PointF(
					(float)Converter.Instance.ConvertInchToMM(-0.255905511811),
					(float)Converter.Instance.ConvertInchToMM(-0.196850393701))));

			GraphicsView.AddShape(DrawingFactory.Instance.GetGdiArc(
				new PointF(
					(float)Converter.Instance.ConvertInchToMM(-0.261415354331),
					(float)Converter.Instance.ConvertInchToMM(-0.255692519685)),
				new PointF(
					(float)Converter.Instance.ConvertInchToMM(-0.261415354331),
					(float)Converter.Instance.ConvertInchToMM(-0.255692519685)),
				new PointF(
					(float)Converter.Instance.ConvertInchToMM(-0.260332677165),
					(float)Converter.Instance.ConvertInchToMM(-0.255692519685))));
		}

		private void LogoView_LogoDoubleClickedEvent(object sender, EventArgs e)
		{
			Processor.Run(DesignFormat.Odb, "D:\\Odb\\21fcb008-01.tgz", "D:\\Odb\\21fcb008-01");
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
