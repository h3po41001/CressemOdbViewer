using CressemLogger;
using CressemLogger.ViewModel;

namespace CressemOdbViewer.ViewModel
{
	public class MainViewModel
	{
		private MainViewModel() { }

		public MainViewModel(LogControlViewModel logView)
		{
			LogView = logView;
			InitLogView();
		}

		public LogControlViewModel LogView { get; private set; }

		private void InitLogView()
		{
			CLogger.Instance.AddInfoLog("Main", "Start Main", true);
			LogView.Referesh();
		}
	}
}
