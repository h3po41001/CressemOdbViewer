using System;
using System.Drawing;
using ImageControl.Model.Gdi;

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

		public float Width { get; private set; }

		public float Height { get; private set; }

		public void InitGraphics(float width, float height)
		{
			Width = width;
			Height = height;
		}

		public void ClearGraphics()
		{
			Width = 0;
			Height = 0;
		}

		public GdiArc GetGdiArc(PointF start, PointF end, PointF center)
		{
			double radius = Math.Sqrt(Math.Pow(start.X - center.X, 2) + Math.Pow(start.Y - center.Y, 2));
			double startAngle = Math.Atan2(start.Y - center.Y, start.X - center.X) * (180 / Math.PI);
			double endAngle = Math.Atan2(end.Y - center.Y, end.X - center.X) * (180 / Math.PI);
			double sweepAngle = (endAngle - startAngle) < 0 ? (endAngle - startAngle) + 360 : (endAngle - startAngle);

			return new GdiArc(new RectangleF(
				(float)(center.X - radius),
				(float)(center.Y - radius),
				(float)(radius * 2)+13, (float)(radius * 2)+13),
				(float)startAngle, (float)sweepAngle);
		}
	}
}
