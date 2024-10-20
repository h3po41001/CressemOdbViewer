using System.Drawing;
using System.Drawing.Drawing2D;

namespace ImageControl.Model.Gdi.Shape
{
	public class GdiLine : GdiShape
	{
		private GdiLine()
		{
		}

		public GdiLine(float x1, float y1, float x2, float y2,
			float pixelResolution) : base(pixelResolution)
		{
			X1 = x1;
			Y1 = y1;
			X2 = x2;
			Y2 = y2;
		}

		public GdiLine(PointF start, PointF end, 
			float pixelResolution) : base(pixelResolution)
		{
			X1 = start.X;
			Y1 = start.Y;
			X2 = end.X;
			Y2 = end.Y;
		}

		public float X1 { get; private set; }

		public float Y1 { get; private set; }

		public float X2 { get; private set; }

		public float Y2 { get; private set; }

		public override void Draw(Graphics graphics, Pen pen)
		{
			graphics.DrawLine(pen, 
				X1 * PixelResolution, Y1 * PixelResolution,
				X2 * PixelResolution, Y2 * PixelResolution);
		}

		public override void AddPath(GraphicsPath path, Pen pen)
		{
			path.AddLine(
				X1 * PixelResolution, Y1 * PixelResolution,
				X2 * PixelResolution, Y2 * PixelResolution);
		}
	}
}
