using System.Windows.Controls;
using CressemFramework.Observer;
using ImageControl.Model;
using ImageControl.Model.DirectX;
using ImageControl.Model.Gdi;
using ImageControl.Shape.Gdi.Interface;
using ImageControl.View.DirectX;
using ImageControl.View.Gdi;

namespace ImageControl.ViewModel
{
	public class GraphicsViewModel : ObservableObject
	{
		private readonly SmartGraphics _graphics = null;
		private readonly Control _graphicsView = null;
		private string _coordinate = string.Empty;

		public GraphicsViewModel() 
		{
			// 변경해야함.. 옵션에 따라
			_graphicsView = new GdiGraphicsView();
			_graphics = new GdiGraphics();
			//_graphicsView = new DirectXView();
			//_graphics = new DirectXGraphics();

			_graphicsView.DataContext = _graphics;
			
			_graphics.Initialize();
			_graphics.MouseMoveEvent += Graphics_MouseMoveEvent;
		}

		public Control GraphicsView { get => _graphicsView; }

		public string Coordinate
		{
			get => _coordinate;
			set
			{
				_coordinate = value;
				OnPropertyChanged();
			}
		}

		public bool LoadProfile(IGdiList profileShape)
		{
			return _graphics.LoadProfile(profileShape);
		}

		public void AddShapes(IGdiList shape)
		{
			_graphics.AddShapes(shape);
		}

		public void ClearShape()
		{
			_graphics.ClearShape();
		}

		private void Graphics_MouseMoveEvent(object sender, object e)
		{
			Coordinate = $"X: {_graphics.MousePos.X}, Y: {_graphics.MousePos.Y}, Zoom: {_graphics.ScreenZoom}";
		}
	}
}
