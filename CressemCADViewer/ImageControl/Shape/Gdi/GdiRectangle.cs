using System.Drawing;
using System.Drawing.Drawing2D;
using ImageControl.Shape.Interface;

namespace ImageControl.Model.Shape.Gdi
{
	internal class GdiRectangle : GdiShape
	{
		private GdiRectangle() { }

		public GdiRectangle(IShapeRectangle shapeRectangle) : 
			base(shapeRectangle.PixelResolution)
		{
			X = shapeRectangle.X;
			Y = -shapeRectangle.Y;
			Width = shapeRectangle.Width;
			Height = shapeRectangle.Height;
		}

		public float X { get; private set; }

		public float Y { get; private set; }

		public float Width { get; private set; }

		public float Height { get; private set; }

		public override void Draw(Graphics graphics)
		{
			graphics.DrawRectangle(DefaultPen, 
				X * PixelResolution,
				Y * PixelResolution,
				Width * PixelResolution,
				Height * PixelResolution);
		}
	}
}
