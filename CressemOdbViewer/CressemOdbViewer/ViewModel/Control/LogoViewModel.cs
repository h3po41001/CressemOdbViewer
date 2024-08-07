using CressemFramework.Command;
using System;
using System.Windows.Input;

namespace CressemOdbViewer.ViewModel.Control
{
	public class LogoViewModel
	{
		public event EventHandler LogoDoubleClickedEvent = delegate { };

		public LogoViewModel()
		{
			DoubleClickLogo = new RelayCommand(() => OnDoubleClickLogo());
		}
		public ICommand DoubleClickLogo { get; private set; }

		private void OnDoubleClickLogo()
		{
			LogoDoubleClickedEvent(this, null);
		}

	}
}
