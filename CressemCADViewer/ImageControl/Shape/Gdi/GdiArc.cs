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

			DefaultPen.Width = LineWidth;
			DefaultPen.Color = Color.DarkGreen;

			ProfilePen.Width = LineWidth;
			ProfilePen.Color = Color.White;
		}

		public float X { get; private set; }

		public float Y { get; private set; }

		public float Width { get; private set; }

		public float Height { get; private set; }

		public float StartAngle { get; private set; }

		public float SweepAngle { get; private set; }

		public float LineWidth { get; private set; }

		public override void Draw(Graphics graphics)
		{
			graphics.DrawArc(DefaultPen, X, Y, Width, Height, StartAngle, SweepAngle);
		}

		public override void DrawProfile(Graphics graphics)
		{
			graphics.DrawArc(ProfilePen, X, Y, Width, Height, StartAngle, SweepAngle);
		}
	}
}
