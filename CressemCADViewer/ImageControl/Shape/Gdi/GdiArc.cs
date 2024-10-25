using System.Drawing;
using ImageControl.Shape.Interface;

namespace ImageControl.Model.Shape.Gdi
{
	internal class GdiArc : GdiShape, IGdiArc
	{
		private GdiArc() { }

		public GdiArc(float x, float y,
			float width, float height,
			float startAngle, float sweepAngle, float lineWidth) : base()
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

		public override void Fill(Graphics graphics)
		{
			graphics.DrawArc(DefaultPen, X, Y, Width, Height, StartAngle, SweepAngle);
		}

		public override void Draw(Graphics graphics)
		{
			graphics.DrawArc(ProfilePen, X, Y, Width, Height, StartAngle, SweepAngle);
		}
	}
}
