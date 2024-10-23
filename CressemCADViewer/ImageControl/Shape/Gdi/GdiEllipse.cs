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
		}

		public float X { get; private set; }

		public float Y { get; private set; }

		public float Width { get; private set; }

		public float Height { get; private set; }

		public override void Draw(Graphics graphics, Matrix _)
		{
			graphics.FillEllipse(new SolidBrush(Color.DarkGreen),
				X * PixelResolution,
				Y * PixelResolution,
				Width * PixelResolution,
				Height * PixelResolution);
		}

		public override void DrawProfile(Graphics graphics)
		{
			DefaultPen.Color = Color.White;
			graphics.DrawEllipse(DefaultPen,
				X * PixelResolution,
				Y * PixelResolution,
				Width * PixelResolution,
				Height * PixelResolution);
		}
	}
}
