using System;
using System.ComponentModel;
using System.Windows.Input;
using CressemCADViewer.Model;
using CressemFramework.Command;
using CressemFramework.Observer;

namespace CressemCADViewer.ViewModel.Control
{
	public class TransformMenuViewModel : ObservableObject
	{
		private bool _isRotateCW0;
		private bool _isRotateCW90;
		private bool _isRotateCW180;
		private bool _isRotateCW270;
		private bool _isFlipVertical;
		private bool _isFlipHorizontal;

		public TransformMenuViewModel()
		{
			_isRotateCW0 = true;
			_isRotateCW90 = false;
			_isRotateCW180 = false;
			_isRotateCW270 = false;
			_isFlipVertical = false;
			_isFlipHorizontal = false;
		}

		public bool IsRotateCW0
		{
			get => _isRotateCW0;
			set
			{
				_isRotateCW0 = value;
				if (_isRotateCW0 is true)
				{
					IsRotateCW90 = false;
					IsRotateCW180 = false;
					IsRotateCW270 = false;
				}
				OnPropertyChanged();
			}
		}

		public bool IsRotateCW90
		{
			get => _isRotateCW90;
			set
			{
				_isRotateCW90 = value;
				if (_isRotateCW90 is true)
				{
					IsRotateCW0 = false;
					IsRotateCW180 = false;
					IsRotateCW270 = false;
				}
				OnPropertyChanged();
			}
		}

		public bool IsRotateCW180
		{
			get => _isRotateCW180;
			set
			{
				_isRotateCW180 = value;
				if (_isRotateCW180 is true)
				{
					IsRotateCW0 = false;
					IsRotateCW90 = false;
					IsRotateCW270 = false;
				}
				OnPropertyChanged();
			}
		}

		public bool IsRotateCW270
		{
			get => _isRotateCW270;
			set
			{
				_isRotateCW270 = value;
				if (_isRotateCW270 is true)
				{
					IsRotateCW0 = false;
					IsRotateCW90 = false;
					IsRotateCW180 = false;
				}
				OnPropertyChanged();
			}
		}

		public bool IsFlipVertical
		{
			get => _isFlipVertical;
			set
			{
				_isFlipVertical = value;
				OnPropertyChanged();
			}
		}

		public bool IsFlipHorizontal
		{
			get => _isFlipHorizontal;
			set
			{
				_isFlipHorizontal = value;
				OnPropertyChanged();
			}
		}
	}
}
