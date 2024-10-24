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

			DefaultPen.Width = LineWidth;
			DefaultPen.Color = Color.DarkGreen;

			ProfilePen.Width = LineWidth;
			ProfilePen.Color = Color.White;
		}

		public float Sx { get; private set; }

		public float Sy { get; private set; }

		public float Ex { get; private set; }

		public float Ey { get; private set; }

		public float LineWidth { get; private set; }

		public override void Draw(Graphics graphics)
		{
			graphics.DrawLine(DefaultPen, Sx, Sy, Ex, Ey);
		}

		public override void DrawProfile(Graphics graphics)
		{
			graphics.DrawLine(ProfilePen, Sx, Sy, Ex, Ey);
		}
	}
}
