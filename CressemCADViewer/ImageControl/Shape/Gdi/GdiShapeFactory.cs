using System.Collections.Generic;
using ImageControl.Model.Shape.Gdi;
using ImageControl.Shape.Gdi.Interface;

namespace ImageControl.Shape.Gdi
{
	internal class GdiShapeFactory
	{
		private static GdiShapeFactory _instance;

		private GdiShapeFactory()
		{
		}

		public static GdiShapeFactory Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new GdiShapeFactory();
				}
				return _instance;
			}
		}

		public GdiShape CreateGdiShape(IGdiShape shapeBase)
		{
			if (shapeBase is IGdiArc arc)
			{
				return new GdiArc(arc.X, arc.Y,
					arc.Width, arc.Height,
					arc.StartAngle, arc.SweepAngle, arc.LineWidth);
			}
			else if (shapeBase is IGdiEllipse ellipse)
			{
				return new GdiEllipse(ellipse.X, ellipse.Y,
					ellipse.Width, ellipse.Height);
			}
			else if (shapeBase is IGdiSurface surface)
			{
				List<GdiShapePolygon> shapes = new List<GdiShapePolygon>();
				foreach (var polygon in surface.Polygons)
				{
					shapes.Add((dynamic)CreateGdiShape(polygon));
				}

				return new GdiSurface(surface.IsPositive, shapes);
			}
			else if (shapeBase is IGdiLine line)
			{
				return new GdiLine(line.Sx, line.Sy, line.Ex, line.Ey, line.LineWidth);
			}
			else if (shapeBase is IGdiRectangle rect)
			{
				return new GdiRectangle(rect.X, rect.Y, rect.Width, rect.Height);
			}
			else if (shapeBase is IGdiPolygon polygon)
			{
				List<GdiShape> shapes = new List<GdiShape>();
				if (polygon.Shapes != null)
				{
					foreach (var shape in polygon.Shapes)
					{
						shapes.Add((dynamic)CreateGdiShape(shape));
					}
				}

				if (polygon.Points != null)
				{
					shapes.Add(new GdiPointsPolygon(polygon.IsFill, polygon.Points));
				}

				return new GdiShapePolygon(polygon.IsFill, shapes);
			}

			return null;
		}
	}
}
