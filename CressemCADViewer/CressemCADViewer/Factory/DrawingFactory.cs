using System;
using System.Drawing;
using CressemCADViewer.Model.Shape;

namespace CressemCADViewer.Factory
{
	internal class DrawingFactory
	{
		private static DrawingFactory _instance;

		private DrawingFactory() { }

		public static DrawingFactory Instance
		{
			get
			{
				if (_instance is null)
				{
					_instance = new DrawingFactory();
				}

				return _instance;
			}
		}

		public ShapeArc GetShapeArc()
		{
			return null;
		}

		public ShapeLine GetShapeLine()
		{
			return null;
		}
	}
}
