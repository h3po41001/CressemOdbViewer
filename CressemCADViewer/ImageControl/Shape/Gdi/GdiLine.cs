using System.Drawing;
using System.Drawing.Drawing2D;

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

			DefaultPen.Color = Color.DarkGreen;

			graphics.DrawLine(DefaultPen, Sx * PixelResolution, Sy * PixelResolution,
				Ex * PixelResolution, Ey * PixelResolution);
		}

		public override void DrawProfile(Graphics graphics)
		{
			LineWidth = 0.1f;
			DefaultPen.Color = Color.White;

			graphics.DrawLine(DefaultPen, Sx * PixelResolution, Sy * PixelResolution,
				Ex * PixelResolution, Ey * PixelResolution);
		}
	}
}
