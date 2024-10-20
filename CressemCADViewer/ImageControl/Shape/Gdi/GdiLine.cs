using System.Drawing;
using System.Drawing.Drawing2D;
using ImageControl.Shape.Interface;

namespace ImageControl.Model.Shape.Gdi
{
	internal class GdiLine : GdiShape
	{
		private GdiLine()
		{
		}

		public GdiLine(IShapeLine shapeLine) : 
			base(shapeLine.PixelResolution)
		{
			Sx = shapeLine.Sx;
			Sy = -shapeLine.Sy;
			Ex = shapeLine.Ex;
			Ey = -shapeLine.Ey;
		}

		public float Sx { get; private set; }

		public float Sy { get; private set; }

		public float Ex { get; private set; }

		public float Ey { get; private set; }

		public override void Draw(Graphics graphics)
		{
			graphics.DrawLine(DefaultPen,
				Sx * PixelResolution, Sy * PixelResolution,
				Ex * PixelResolution, Ey * PixelResolution);
		}
	}
}
