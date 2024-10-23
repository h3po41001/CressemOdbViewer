using System.Drawing;
using System.Drawing.Drawing2D;

namespace ImageControl.Model.Shape.Gdi
{
	internal class GdiArc : GdiShape
	{
		private GdiArc() { }

		public GdiArc(float pixelResolution,
			float x, float y,
			float width, float height,
			float startAngle, float sweepAngle, float lineWidth) : base(pixelResolution)
		{
			X = x;
			Y = y;
			Width = width;
			Height = height;
			StartAngle = startAngle;
			SweepAngle = sweepAngle;
			LineWidth = lineWidth;
		}

		public float X { get; private set; }

		public float Y { get; private set; }

		public float Width { get; private set; }

		public float Height { get; private set; }

		public float StartAngle { get; private set; }

		public float SweepAngle { get; private set; }

		public float LineWidth { get; private set; }

		public override void Draw(Graphics graphics, Matrix _)
		{
			if (LineWidth > 0)
			{
				DefaultPen.Width = LineWidth * PixelResolution;
			}

			DefaultPen.Color = Color.DarkGreen;
			graphics.DrawArc(
				DefaultPen,
				X * PixelResolution,
				Y * PixelResolution,
				Width * PixelResolution, 
				Height * PixelResolution,
				StartAngle, 
				SweepAngle);
		}

		public override void DrawProfile(Graphics graphics)
		{
			DefaultPen.Color = Color.White;
			graphics.DrawArc(
				DefaultPen,
				X * PixelResolution,
				Y * PixelResolution,
				Width * PixelResolution,
				Height * PixelResolution,
				StartAngle,
				SweepAngle);
		}
	}
}
