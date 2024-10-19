using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using CressemFramework.Command;
using CressemFramework.Observer;

namespace CressemCADViewer.ViewModel.Control
{
	public class PropertyViewModel : ObservableObject
	{
		public event RoutedEventHandler SelectedStepChangedEvent = delegate { };
		public event RoutedEventHandler SelectedLayerChangedEvent = delegate { };
		public event RoutedEventHandler LoadCamImageEvent = delegate { };

		private string[] _stepNames = null;
		private string[] _layerNames = null;

		public PropertyViewModel()
		{
			_stepNames = new string[] { "" };
			_layerNames = new string[] { "" };

			SelectedStepChangedCommand = new RelayCommand(() => { SelectedStepChangedEvent(this, null); });
			SelectedLayerChangedCommand = new RelayCommand(() => { SelectedLayerChangedEvent(this, null); });
			LoadCamImageCommand = new RelayCommand(() => { LoadCamImageEvent(this, null); });
		}

		[Browsable(false)]
		public ICommand SelectedStepChangedCommand { get; private set; }

		[Browsable(false)]
		public ICommand SelectedLayerChangedCommand { get; private set; }

		[Browsable(false)]
		public ICommand LoadCamImageCommand { get; private set; }

		[Browsable(false)]
		public string SelectedStepName { get; set; }

		public string[] StepNames
		{
			get => _stepNames;
			set
			{
				_stepNames = value;
				OnPropertyChanged();
			}
		}

		[Browsable(false)]
		public string SelectedLayerName { get; set; }

		public string[] LayerNames
		{
			get => _layerNames;
			set
			{
				_layerNames = value;
				OnPropertyChanged();
			}
		}
	}
}
