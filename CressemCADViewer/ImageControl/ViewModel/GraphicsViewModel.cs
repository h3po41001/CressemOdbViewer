﻿using System.Collections.Generic;
using System.Windows.Controls;
using CressemFramework.Observer;
using ImageControl.Model;
using ImageControl.Model.DirectX;
using ImageControl.Model.Gdi;
using ImageControl.Shape.Interface;
using ImageControl.View.DirectX;
using ImageControl.View.Gdi;

namespace ImageControl.ViewModel
{
	public class GraphicsViewModel : ObservableObject
	{
		private SmartGraphics _graphics = null;
		private Control _graphicsView = null;
		private string _coordinate = string.Empty;

		private GraphicsViewModel() { }

		public GraphicsViewModel(GraphicsType graphicsType)
		{
			if (graphicsType is GraphicsType.GdiPlus)
			{
				_graphicsView = new GdiGraphicsView();
				_graphics = new GdiGraphics();
			}
			else if (graphicsType is GraphicsType.DirectX)
			{
				_graphicsView = new DirectXView();
				_graphics = new DirectXGraphics();
			}
			else
			{
				return;
			}

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

		public bool LoadProfiles(IEnumerable<IGraphicsList> profileShapes)
		{
			return _graphics.LoadProfiles(profileShapes);
		}

		public void AddShapes(IGraphicsList shapes)
		{
			_graphics.AddShapes(shapes);
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
