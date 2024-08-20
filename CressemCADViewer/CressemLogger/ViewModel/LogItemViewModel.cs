using CressemFramework.Observer;
using CressemLogger.Model;
using System.Collections.ObjectModel;

namespace CressemLogger.ViewModel
{
	public class LogItemViewModel : ObservableObject
	{
		private string _name = string.Empty;
		private readonly int _maxLogCount = 1000;

		private readonly ObservableCollection<LogDisplay> _outputLogMessages =
			new ObservableCollection<LogDisplay>();

		public LogItemViewModel()
		{
		}

		public string Name
		{
			get => _name;
			set
			{
				_name = value;
				OnPropertyChanged();
			}
		}

		public ObservableCollection<LogDisplay> LogMessages
		{
			get => _outputLogMessages;
		}

		public void AddLog(LogDisplay log)
		{
			_outputLogMessages.Add(log);

			if (_outputLogMessages.Count > _maxLogCount)
			{
				_outputLogMessages.RemoveAt(0);
			}		
		}

		public void UpdateLogView()
		{
			OnPropertyChanged(nameof(LogMessages));
		}
	}
}
