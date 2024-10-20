using System.Drawing;
using System.Drawing.Drawing2D;

namespace ImageControl.Model.Gdi.Shape
{
	internal class GdiEllipse : GdiShape
	{
		private GdiEllipse() { }

		public GdiEllipse(float x, float y, float width, float height, 
			float pixelResolution) : base(pixelResolution)
		{
			X = x;
			Y = y;
			Width = width;
			Height = height;
		}

		public GdiEllipse(RectangleF rectangle, 
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

		public override void Draw(Graphics graphics, Pen pen)
		{
			graphics.DrawEllipse(pen,
				X * PixelResolution,
				Y * PixelResolution,
				Width * PixelResolution,
				Height * PixelResolution);
		}

		public override void AddPath(GraphicsPath path, Pen pen)
		{
			throw new System.NotImplementedException();
		}
	}
}
