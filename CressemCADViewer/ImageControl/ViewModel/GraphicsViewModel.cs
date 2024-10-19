using System;
using System.Drawing;
using System.Windows.Controls;
using ImageControl.Model;
using ImageControl.Model.Gdi;
using ImageControl.View.Gdi;

namespace ImageControl.ViewModel
{
	public class GraphicsViewModel
	{
		private SmartGraphics _graphics = null;
		private Control _graphicsView = null;

		public GraphicsViewModel() 
		{
			_graphicsView = new GdiGraphicsView();
			_graphics = new GdiGraphics();
			
			_graphicsView.DataContext = _graphics;
			_graphics.Initialize();
		}

		public Control GraphicsView { get => _graphicsView; }

		public bool LoadImage(Bitmap image)
		{
			return _graphics.LoadImage(image);
		}
	}
}
