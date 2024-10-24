using System.Drawing;
using System.Drawing.Drawing2D;

namespace ImageControl.Model.Shape.Gdi
{
	internal class GdiEllipse : GdiShape
	{
		private GdiEllipse() { }

		public GdiEllipse(float pixelResolution,
			float x, float y,
			float width, float height) : base(pixelResolution)
		{
			X = x;
			Y = y;
			Width = width;
			Height = height;
			ProfilePen.Color = Color.White;
		}

		public float X { get; private set; }

		public float Y { get; private set; }

		public float Width { get; private set; }

		public float Height { get; private set; }

		public override void Draw(Graphics graphics)
		{
			graphics.FillEllipse(SolidBrush, X, Y, Width, Height);
		}

		public override void DrawProfile(Graphics graphics)
		{
			graphics.DrawEllipse(ProfilePen, X, Y, Width, Height);
		}
	}
}
