using System.Drawing;
using System.Drawing.Drawing2D;
using ImageControl.Model.Shape.Gdi;

namespace ImageControl.Extension
{
	internal static class GdiObjectExtension
	{
		public static void Add(this GraphicsPath path,
			GdiShape gdiBase, float mul = 1.0f)
		{
			if (gdiBase is GdiLine gdiLine)
			{
				path.AddLine(gdiLine, mul);
			}
			else if (gdiBase is GdiRectangle gdiRectangle)
			{
				path.AddRectangle(gdiRectangle, mul);
			}
			else if (gdiBase is GdiEllipse gdiEllipse)
			{
				path.AddEllipse(gdiEllipse, mul);
			}
			else if (gdiBase is GdiArc gdiArc)
			{
				path.AddArc(gdiArc, mul);
			}
			else
			{
				throw new System.NotImplementedException();
			}
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
	}
}
