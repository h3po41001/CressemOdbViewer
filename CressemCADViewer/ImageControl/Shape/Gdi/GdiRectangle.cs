using System.Drawing;
using System.Drawing.Drawing2D;

namespace ImageControl.Model.Shape.Gdi
{
	internal class GdiRectangle : GdiShape
	{
		private GdiRectangle() { }

		public GdiRectangle(float pixelResolution,
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
			graphics.FillRectangle(SolidBrush, X, Y, Width, Height);
		}

		public override void DrawProfile(Graphics graphics)
		{
			graphics.DrawRectangle(ProfilePen, X, Y, Width, Height);
		}
	}
}
