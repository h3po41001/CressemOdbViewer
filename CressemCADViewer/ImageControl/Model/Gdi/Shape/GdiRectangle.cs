using System.Drawing;
using System.Drawing.Drawing2D;

namespace ImageControl.Model.Gdi.Shape
{
	internal class GdiRectangle : GdiShape
	{
		private GdiRectangle() { }

		public GdiRectangle(float x, float y, float width, float height, 
			float pixelResolution) : base(pixelResolution)
		{
			X = x;
			Y = y;
			Width = width;
			Height = height;
		}

		public GdiRectangle(RectangleF rectangle, 
			float pixelResolution) : base(pixelResolution)
		{
			X = rectangle.X;
			Y = rectangle.Y;
			Width = rectangle.Width;
			Height = rectangle.Height;
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

		public override void AddPath(GraphicsPath path)
		{
			throw new System.NotImplementedException();
		}
	}
}
