﻿using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using ImageControl.Model.Shape.Gdi;
using ImageControl.Shape.Gdi;

namespace ImageControl.Extension
{
	internal static class GdiObjectExtension
	{
		public static void Add(this GraphicsPath path, GdiShape gdiBase)
		{
			if (gdiBase is GdiLine gdiLine)
			{
				path.AddLine(gdiLine);
			}
			else if (gdiBase is GdiRectangle gdiRectangle)
			{
				path.AddRectangle(gdiRectangle);
			}
			else if (gdiBase is GdiEllipse gdiEllipse)
			{
				path.AddEllipse(gdiEllipse);
			}
			else if (gdiBase is GdiArc gdiArc)
			{
				path.AddArc(gdiArc);
			}
			else if (gdiBase is GdiPointsPolygon gdiPolygon)
			{
				path.AddPolygon(gdiPolygon);
			}
			//else if (gdiBase is GdiShapePolygon gdiShapePolygon)
			//{
			//	foreach (var shape in gdiShapePolygon.Shapes)
			//	{
			//		path.Add(shape, mul);
			//	}
			//}
			//else if (gdiBase is GdiSurface gdiSurface)
			//{
			//	foreach (var polygon in gdiSurface.Polygons)
			//	{
			//		path.Add(polygon, mul);
			//	}
			//}
		}

		private static void AddArc(this GraphicsPath path,
			GdiArc arc, float mul = 1.0f)
		{
			path.AddArc(arc.X * mul, arc.Y * mul,
				arc.Width * mul, arc.Height * mul,
				arc.StartAngle, arc.SweepAngle);
		}

		public static void AddLine(this GraphicsPath path,
			GdiLine line, float mul = 1.0f)
		{
			path.AddLine(line.Sx * mul, line.Sy * mul,
				line.Ex * mul, line.Ey * mul);
		}

		public static void AddEllipse(this GraphicsPath path,
			GdiEllipse ellipse, float mul = 1.0f)
		{
			path.AddEllipse(ellipse.X * mul, ellipse.Y * mul,
				ellipse.Width * mul, ellipse.Height * mul);
		}

		public static void AddRectangle(this GraphicsPath path,
			GdiRectangle rectangle, float mul = 1.0f)
		{
			path.AddRectangle(new RectangleF(
				rectangle.X * mul, rectangle.Y * mul,
				rectangle.Width * mul, rectangle.Height * mul));
		}

		public static void AddPolygon(this GraphicsPath path,
			GdiPointsPolygon polygon, float mul = 1.0f)
		{
			path.AddPolygon(polygon.Points.Select(
				x => new PointF(x.X * mul, x.Y * mul)).ToArray());
		}
	}
}
