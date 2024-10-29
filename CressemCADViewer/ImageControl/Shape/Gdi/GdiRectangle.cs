using System.Drawing;
using System.Drawing.Drawing2D;
using ImageControl.Shape.Gdi.Interface;

namespace ImageControl.Model.Shape.Gdi
{
	internal class GdiRectangle : GdiShape
	{
		private GdiRectangle() :base() { }

		public GdiRectangle(bool isPositive, 
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
				graphics.FillRectangle(SolidBrush, X, Y, Width, Height);
			}
			else
			{
				graphics.FillRectangle(HoleBrush, X, Y, Width, Height);
			}
		}

		public override void Draw(Graphics graphics)
		{
			graphics.DrawRectangle(ProfilePen, X, Y, Width, Height);
		}
	}
}
