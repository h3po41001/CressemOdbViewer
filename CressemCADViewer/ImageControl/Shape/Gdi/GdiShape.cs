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
		}

		public float PixelResolution { get; private set; } = 1.0f;

		protected Pen DefaultPen { get; private set; }

		public abstract void Draw(Graphics graphics, Matrix matrix = null);

		public abstract void DrawProfile(Graphics graphics);
	}
}
