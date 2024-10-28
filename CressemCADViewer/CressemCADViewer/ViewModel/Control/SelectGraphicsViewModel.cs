using CressemFramework.Observer;
using ImageControl.Model;

namespace CressemCADViewer.ViewModel.Control
{
	internal class SelectGraphicsViewModel : ObservableObject
	{
		private GraphicsType _graphicsType = GraphicsType.None;

		public SelectGraphicsViewModel()
		{
		}

		public GraphicsType GraphicsType
		{
			get => _graphicsType;
			set
			{
				_graphicsType = value;
				OnPropertyChanged();
			}
		}
	}
}
