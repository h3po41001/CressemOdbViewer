using System.Collections.Generic;
using System.Windows.Documents;
using ImageControl.Model.Shape.Gdi;
using ImageControl.Shape.Gdi;
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
				return new GdiArc(arc.PixelResolution,
					arc.X, arc.Y,
					arc.Width, arc.Height,
					arc.StartAngle, arc.SweepAngle);
			}
			else if (shapeBase is IShapeEllipse ellipse)
			{
				return new GdiEllipse(ellipse.PixelResolution,
					ellipse.X, ellipse.Y,
					ellipse.Width, ellipse.Height);
			}
			else if (shapeBase is IShapeSurface surface)
			{
				List<GdiPolygon> shapes = new List<GdiPolygon>();
				foreach (var polygon in surface.Polygons)
				{
					shapes.Add((dynamic)CreateGdiShape(polygon));
				}

				return new GdiSurface(surface.PixelResolution,
					surface.IsPositive, shapes);
			}
			else if (shapeBase is IShapeLine line)
			{
				return new GdiLine(line.PixelResolution,
					line.Sx, line.Sy, line.Ex, line.Ey);
			}
			else if (shapeBase is IShapeRectangle rect)
			{
				return new GdiRectangle(rect.PixelResolution,
					rect.X, rect.Y, rect.Width, rect.Height);
			}
			else if (shapeBase is IShapePolygon polygon)
			{
				List<GdiShape> shapes = new List<GdiShape>();
				foreach (var shape in polygon.Shapes)
				{
					shapes.Add((dynamic)CreateGdiShape(shape));
				}

				return new GdiPolygon(polygon.PixelResolution,
					polygon.IsFill, shapes);
			}

			return null;
		}
	}
}
