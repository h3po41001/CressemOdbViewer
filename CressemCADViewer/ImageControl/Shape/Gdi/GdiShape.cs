using System.Drawing;
using System.Drawing.Drawing2D;

namespace ImageControl.Model.Shape.Gdi
{
	internal abstract class GdiShape
	{
		protected GdiShape() { }

		protected GdiShape(float pixelResolution)
		{
			PixelResolution = pixelResolution;
			DefaultPen = new Pen(Color.White, 0.1f);
			GraphicsPath = new GraphicsPath();
		}

		public float PixelResolution { get; private set; } = 1.0f;

		public GraphicsPath GraphicsPath { get; private set; }

		protected Pen DefaultPen { get; private set; }

		public abstract void Draw(Graphics graphics);

		public abstract void DrawProfile(Graphics graphics);
	}
}
