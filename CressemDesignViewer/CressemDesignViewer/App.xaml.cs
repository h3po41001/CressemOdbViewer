using CressemLogger;
using CressemLogger.ViewModel;
using CressemDesignViewer.ViewModel;
using System.IO;
using System.Windows;

namespace CressemDesignViewer
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
				if (_mainViewModel == null)
				{
					_mainViewModel = new MainViewModel(_logView);
				}

				return _mainViewModel;
			}
		}

		private void Application_Startup(object sender, StartupEventArgs e)
		{
			_logView = new LogControlViewModel(Current.Dispatcher);

			var folderPath = Directory.GetCurrentDirectory();
			CLogger.Instance.Initialize(folderPath + "\\Log");

			_logView.AddLogClass("App");
			CLogger.Instance.AddInfoLog("App", "Start Application", true);

			MainWindow = new MainWindow
			{
				DataContext = MainViewModel,
			};

			MainWindow.ShowDialog();
		}
	}
}
