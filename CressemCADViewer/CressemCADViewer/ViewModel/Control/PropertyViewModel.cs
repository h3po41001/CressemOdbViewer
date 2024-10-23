using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using CressemFramework.Command;
using CressemFramework.Observer;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace CressemCADViewer.ViewModel.Control
{
	public class PropertyViewModel : ObservableObject
	{
		public event RoutedEventHandler SelectedStepChangedEvent = delegate { };
		public event RoutedEventHandler SelectedLayerChangedEvent = delegate { };
		public event RoutedEventHandler ExtractEvent = delegate { };
		public event RoutedEventHandler LoadCamImageEvent = delegate { };

		private Window _parentWindow = null;
		private string _odbLoadPath = string.Empty;
		private string[] _stepNames = null;
		private string[] _layerNames = null;

		public PropertyViewModel(Window parentWindow)
		{
			_parentWindow = parentWindow;
			_stepNames = new string[] { "" };
			_layerNames = new string[] { "" };

			ClickOdbLoadPathCommand = new RelayCommand(() => OnClickOdbLoadPath());
			SelectedStepChangedCommand = new RelayCommand(() => SelectedStepChangedEvent(this, null));
			SelectedLayerChangedCommand = new RelayCommand(() => SelectedLayerChangedEvent(this, null));
			ExtractCommand = new RelayCommand(() => ExtractEvent(this, null));
			LoadCamImageCommand = new RelayCommand(() => LoadCamImageEvent(this, null));
		}

		[Browsable(false)]
		public ICommand ClickOdbLoadPathCommand { get; private set; }

		[Browsable(false)]
		public ICommand SelectedStepChangedCommand { get; private set; }

		[Browsable(false)]
		public ICommand SelectedLayerChangedCommand { get; private set; }

		[Browsable(false)]
		public ICommand ExtractCommand { get; private set; }

		[Browsable(false)]
		public ICommand LoadCamImageCommand { get; private set; }		

		public string OdbLoadPath
		{
			get => _odbLoadPath;
			set
			{
				_odbLoadPath = value;
				OnPropertyChanged();
			}
		}

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

		private void OnClickOdbLoadPath()
		{
			CommonOpenFileDialog openFileDialog = new CommonOpenFileDialog()
			{
				IsFolderPicker = false,
			};

			if (openFileDialog.ShowDialog(_parentWindow) == CommonFileDialogResult.Ok)
			{
				OdbLoadPath = openFileDialog.FileName;
			}
		}
	}
}
