using System.Drawing;

namespace ImageControl.Model.Shape.Gdi
{
	internal class GdiEllipse : GdiShape
	{
		private GdiEllipse() : base() { }

		public GdiEllipse(bool isPositive,
			float x, float y,
			float width, float height) : base(isPositive)
		{
			X = x;
			Y = y;
			Width = width;
			Height = height;
			ProfilePen.Color = Color.White;

			HoleBrush = new SolidBrush(Color.Black);
		}

		public float X { get; private set; }

		public float Y { get; private set; }

		public float Width { get; private set; }

		public float Height { get; private set; }

		public SolidBrush HoleBrush { get; private set; }

		public override void Fill(Graphics graphics)
		{
			if (IsPositive)
			{
				graphics.FillEllipse(SolidBrush, X, Y, Width, Height);
			}
			else
			{
				graphics.FillEllipse(HoleBrush, X, Y, Width, Height);
			}
		}

		public override void Draw(Graphics graphics)
		{
			graphics.DrawEllipse(ProfilePen, X, Y, Width, Height);
		}
	}
}
