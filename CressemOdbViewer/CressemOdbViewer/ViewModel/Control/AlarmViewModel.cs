using CressemFramework.Observer;
using CressemDesignViewer.Model;
using System.Drawing;

namespace CressemDesignViewer.ViewModel.Control
{
	public class AlarmViewModel : ObservableObject
	{
		private string _alarmColor = Color.Green.ToKnownColor().ToString();
		private ProcessState _state = ProcessState.Idle;

		public AlarmViewModel()
		{
		}

		public string AlarmColor { get => _alarmColor; }

		public string AlarmText { get => _state.ToString().ToUpper(); }

		public ProcessState GetState() => _state;

		public void SetState(ProcessState state, Color color)
		{
			_state = state;
			_alarmColor = color.ToKnownColor().ToString();

			OnPropertyChanged("AlarmColor");
			OnPropertyChanged("AlarmText");
		}
	}
}
