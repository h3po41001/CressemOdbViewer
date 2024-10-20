using System.Drawing;
using System.Drawing.Drawing2D;

namespace ImageControl.Model.Gdi.Shape
{
	public class GdiArc : GdiShape
	{
		private GdiArc() { }

		public GdiArc(float x, float y, float width, float height,
			float startAngle, float sweepAngle,
			float pixelResolution) : base(pixelResolution)
		{
			X = x;
			Y = y;
			Width = width;
			Height = height;
			StartAngle = startAngle;
			SweepAngle = sweepAngle;
		}

		public GdiArc(RectangleF boundary,
			float startAngle, float sweepAngle,
			float pixelResolution) : base(pixelResolution)
		{
			X = boundary.X;
			Y = boundary.Y;
			Width = boundary.Width;
			Height = boundary.Height;
			StartAngle = startAngle;
			SweepAngle = sweepAngle;
		}

		public float X { get; private set; }

		public float Y { get; private set; }

		public float Width { get; private set; }

		public float Height { get; private set; }

		public float StartAngle { get; private set; }

		public float SweepAngle { get; private set; }

		public override void Draw(Graphics graphics, Pen pen)
		{
			graphics.DrawArc(pen,
				X * PixelResolution, Y * PixelResolution,
				Width * PixelResolution, Height * PixelResolution,
				StartAngle, SweepAngle);
		}

		public override void AddPath(GraphicsPath path, Pen pen)
		{
			path.AddArc(
				X * PixelResolution, Y * PixelResolution,
				Width * PixelResolution, Height * PixelResolution,
				StartAngle, SweepAngle);
		}
	}
}
