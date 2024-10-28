using System.Collections.Generic;
using ImageControl.Model.Shape.Gdi;
using ImageControl.Shape.Gdi.Interface;
using SharpDX.DXGI;

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

		public GdiShape CreateGdiShape(IGdiArc arc)
		{
			if (arc is null)
			{
				return null;
			}

			return new GdiArc(arc.X, arc.Y,
				arc.Width, arc.Height,
				arc.StartAngle, arc.SweepAngle, arc.LineWidth);
		}

		public GdiShape CreateGdiShape(IGdiEllipse ellipse)
		{
			if (ellipse is null)
			{
				return null;
			}

			return new GdiEllipse(ellipse.X, ellipse.Y,
				ellipse.Width, ellipse.Height);
		}

		public GdiShape CreateGdiShape(IGdiLine line)
		{
			if (line is null)
			{
				return null;
			}

			return new GdiLine(line.Sx, line.Sy, line.Ex, line.Ey, line.LineWidth);
		}

		public GdiShape CreateGdiShape(IGdiPolygon polygon)
		{
			if (polygon is null)
			{
				return null;
			}

			List<GdiShape> shapes = new List<GdiShape>();
			if (polygon.Shapes != null)
			{
				foreach (var shape in polygon.Shapes)
				{
					shapes.Add(CreateGdiShape((dynamic)shape));
				}
			}

			if (polygon.Points != null)
			{
				shapes.Add(new GdiPointsPolygon(polygon.IsFill, polygon.Points));
			}

			return new GdiShapePolygon(polygon.IsFill, shapes);
		}

		public GdiShape CreateGdiShape(IGdiRectangle rect)
		{
			if (rect is null)
			{
				return null;
			}

			return new GdiRectangle(rect.X, rect.Y, rect.Width, rect.Height);
		}

		public GdiShape CreateGdiShape(IGdiText text)
		{
			if (text is null)
			{
				return null;
			}

			return null;
			//return new GdiText(text.X, text.Y, text.Text, text.Font, text.Brush);
		}

		public GdiShape CreateGdiShape(IGdiSurface surface)
		{
			if (surface is null)
			{
				return null;
			}

			List<GdiShapePolygon> shapes = new List<GdiShapePolygon>();
			foreach (var polygon in surface.Polygons)
			{
				shapes.Add((dynamic)CreateGdiShape(polygon));
			}

			return new GdiSurface(surface.IsPositive, shapes);
		}
	}
}
