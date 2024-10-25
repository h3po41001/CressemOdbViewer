using System.Drawing;
using System.Drawing.Drawing2D;
using ImageControl.Shape.Gdi.Interface;

namespace ImageControl.Model.Shape.Gdi
{
	internal class GdiRectangle : GdiShape, IGdiRectangle
	{
		private GdiRectangle() { }

		public GdiRectangle(float x, float y,
			float width, float height) : base()
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

		public override void Fill(Graphics graphics)
		{
			graphics.FillRectangle(SolidBrush, X, Y, Width, Height);
		}

		public override void Draw(Graphics graphics)
		{
			graphics.DrawRectangle(ProfilePen, X, Y, Width, Height);
		}
	}
}
