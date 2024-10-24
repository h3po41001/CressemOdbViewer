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
			SolidBrush = new SolidBrush(Color.DarkGreen);
			DefaultPen = new Pen(Color.White, 0.1f);
			ProfilePen = new Pen(Color.White, 0.1f);
		}

		public float PixelResolution { get; private set; } = 1.0f;

		protected SolidBrush SolidBrush { get; private set; }

		protected Pen DefaultPen { get; private set; }

		protected Pen ProfilePen { get; private set; }

		public abstract void Draw(Graphics graphics);

		public abstract void DrawProfile(Graphics graphics);
	}
}
