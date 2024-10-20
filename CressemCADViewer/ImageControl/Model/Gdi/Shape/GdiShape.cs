using System.Drawing;
using System.Drawing.Drawing2D;

namespace ImageControl.Model.Gdi.Shape
{
	public abstract class GdiShape
	{
		protected GdiShape() { }

		protected GdiShape(float pixelResolution)
		{
			PixelResolution = pixelResolution;
			DefaultPen = new Pen(Color.White, 1f);
		}

		protected float PixelResolution { get; private set; }

		protected Pen DefaultPen { get; private set; }

		public abstract void Draw(Graphics graphics);

		public abstract void AddPath(GraphicsPath path);
	}
}
