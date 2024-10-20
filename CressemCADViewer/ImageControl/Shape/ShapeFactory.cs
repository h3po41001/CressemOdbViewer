using ImageControl.Model.Shape.Gdi;
using ImageControl.Shape.Interface;

namespace ImageControl.Shape
{
	internal class ShapeFactory
	{
		private static ShapeFactory _instance;

		private ShapeFactory()
		{
		}

		public static ShapeFactory Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new ShapeFactory();
				}
				return _instance;
			}
		}

		public GdiShape CreateGdiShape(IShapeBase shapeBase)
		{
			if (shapeBase is IShapeArc arc)
			{
				return new GdiArc(arc);
			}
			else if (shapeBase is IShapeEllipse ellipse)
			{
				return new GdiEllipse(ellipse);
			}
			else if (shapeBase is IShapePolygon surface)
			{
				return new GdiGraphicsPath(surface);
			}
			else if (shapeBase is IShapeLine line)
			{
				return new GdiLine(line);
			}
			else if (shapeBase is IShapeRectangle rect)
			{
				return new GdiRectangle(rect);
			}

			return null;
		}
	}
}
