using System.Drawing;
using System.Drawing.Drawing2D;
using ImageControl.Shape.Interface;

namespace ImageControl.Model.Shape.Gdi
{
	internal abstract class GdiShape
	{
		protected GdiShape() { }

		protected GdiShape(float pixelResolution)
		{
			PixelResolution = pixelResolution;
			DefaultPen = new Pen(Color.White, 1f);
		}

		public float PixelResolution { protected get; set; } = 1.0f;

		protected Pen DefaultPen { get; private set; }

		public abstract void Draw(Graphics graphics);
	}
}
