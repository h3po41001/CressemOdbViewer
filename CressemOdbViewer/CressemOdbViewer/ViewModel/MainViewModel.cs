using CressemLogger;
using CressemLogger.ViewModel;
using CressemOdbViewer.ViewModel.Control;

namespace CressemOdbViewer.ViewModel
{
	public class MainViewModel
	{
		private MainViewModel() { }

		public MainViewModel(LogControlViewModel logView)
		{
			LogView = logView;

			AlarmView = new AlarmViewModel();
			LogoView = new LogoViewModel();

			InitLogView();
		}

		public LogControlViewModel LogView { get; private set; }

		public AlarmViewModel AlarmView { get; private set; }

		public LogoViewModel LogoView { get; private set; }

		private void InitLogView()
		{
			CLogger.Instance.AddInfoLog("Main", "Start Main", true);
			LogView.Referesh();
		}
	}
}
