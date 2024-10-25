using System.Drawing;
using System.Drawing.Drawing2D;
using ImageControl.Shape.Gdi.Interface;

namespace ImageControl.Model.Shape.Gdi
{
	internal class GdiEllipse : GdiShape, IGdiEllipse
	{
		private GdiEllipse() { }

		public GdiEllipse(float x, float y,
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
			graphics.FillEllipse(SolidBrush, X, Y, Width, Height);
		}

		public override void Draw(Graphics graphics)
		{
			graphics.DrawEllipse(ProfilePen, X, Y, Width, Height);
		}
	}
}
