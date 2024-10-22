using System.Drawing;
using System.Drawing.Drawing2D;
using ImageControl.Shape.Interface;

namespace ImageControl.Model.Shape.Gdi
{
	internal class GdiLine : GdiShape
	{
		private GdiLine() : base()
		{
		}

		public GdiLine(float pixelResolution,
			float sx, float sy,
			float ex, float ey, float width) : base(pixelResolution)
		{
			Sx = sx;
			Sy = sy;
			Ex = ex;
			Ey = ey;
			LineWidth = width;
		}

		public float Sx { get; private set; }

		public float Sy { get; private set; }

		public float Ex { get; private set; }

		public float Ey { get; private set; }

		public float LineWidth { get; private set; }

		public override void Draw(Graphics graphics)
		{
			if (LineWidth > 0)
			{
				DefaultPen.Width = LineWidth * PixelResolution;
			}

			graphics.DrawLine(DefaultPen, Sx * PixelResolution, Sy * PixelResolution,
				Ex * PixelResolution, Ey * PixelResolution);
		}
	}
}
