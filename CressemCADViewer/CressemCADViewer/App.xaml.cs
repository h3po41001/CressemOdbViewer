using CressemLogger;
using CressemLogger.ViewModel;
using CressemCADViewer.ViewModel;
using System.IO;
using System.Windows;
using CressemCADViewer.ViewModel.Control;
using CressemCADViewer.View.Control;
using ImageControl.Model;

namespace CressemCADViewer
{
	/// <summary>
	/// App.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class App : Application
	{
		private static MainViewModel _mainViewModel;
		private static LogControlViewModel _logView;

		public static MainViewModel MainViewModel
		{
			get
			{
				return _mainViewModel;
			}
		}

		private void Application_Startup(object sender, StartupEventArgs e)
		{
			ShutdownMode = ShutdownMode.OnExplicitShutdown;
			_logView = new LogControlViewModel(Current.Dispatcher);
			
			var folderPath = Directory.GetCurrentDirectory();
			CLogger.Instance.Initialize(folderPath + "\\Log");

			_logView.AddLogClass("App");
			CLogger.Instance.AddInfoLog("App", "Start Application", true);

			SelectGraphicsViewModel selectGraphicsVm = new SelectGraphicsViewModel();
			SelectGraphicsView selectGraphicsView = new SelectGraphicsView()
			{
				DataContext = selectGraphicsVm
			};

			if (selectGraphicsView.ShowDialog() is false)
			{
				Shutdown();
				return;
			}

			if (selectGraphicsVm.GraphicsType is GraphicsType.None)
			{
				MessageBox.Show("Please select graphics type.");
				Shutdown();
				return;
			}

			ShutdownMode = ShutdownMode.OnMainWindowClose;
			MainWindow = new MainWindow();
			
			_mainViewModel = new MainViewModel(MainWindow, selectGraphicsVm.GraphicsType, _logView);
			MainWindow.DataContext = _mainViewModel;

			MainWindow.ShowDialog();
		}
	}
}
