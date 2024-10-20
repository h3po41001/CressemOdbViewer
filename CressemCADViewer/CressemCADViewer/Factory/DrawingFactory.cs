using System;
using System.Drawing;
using ImageControl.Model.Gdi.Shape;

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

		public RectangleF GetGdiRoi(RectangleF roi)
		{
			return new RectangleF(roi.X, -roi.Y, roi.Width, roi.Height);
		}

		public GdiLine GetGdiLine(PointF start, PointF end)
		{
			return new GdiLine(
				start.X, -start.Y, 
				end.X, -end.Y, 10.0f);
		}

		public GdiArc GetGdiArc(PointF start, PointF end, PointF center)
		{
			double radius = Math.Sqrt(Math.Pow(start.X - center.X, 2) + Math.Pow(-(start.Y - center.Y), 2));
			double startAngle = Math.Atan2(-(start.Y - center.Y), start.X - center.X) * (180 / Math.PI);
			double endAngle = Math.Atan2(-(end.Y - center.Y), end.X - center.X) * (180 / Math.PI);
			double sweepAngle = (endAngle - startAngle) <= 0 ? (endAngle - startAngle) + 360 : (endAngle - startAngle);

			return new GdiArc(
				(float)(center.X - radius),
				(float)(-center.Y - radius),
				(float)(radius * 2), (float)(radius * 2),
				(float)startAngle, (float)sweepAngle, 10.0f);
		}
	}
}
