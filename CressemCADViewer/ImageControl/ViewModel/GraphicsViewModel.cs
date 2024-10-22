using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Controls;
using CressemFramework.Observer;
using ImageControl.Model;
using ImageControl.Model.Gdi;
using ImageControl.Shape.Interface;
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
			_graphicsView = new GdiGraphicsView();
			_graphics = new GdiGraphics();
			
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

		public bool LoadRoi(IShapeBase roiShape)
		{
			return _graphics.LoadRoi(roiShape);
		}

		public void AddShapes(IEnumerable<IShapeBase> shape)
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
