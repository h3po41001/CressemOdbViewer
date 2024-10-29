using System.Drawing;
using System.Drawing.Drawing2D;

namespace ImageControl.Model.Shape.Gdi
{
	internal class GdiLine : GdiShape
	{
		private GdiLine() : base() { }

		public GdiLine(bool isPositive, 
			float sx, float sy,
			float ex, float ey, 
			float lineWidth) : base(isPositive)
		{
			Sx = sx;
			Sy = sy;
			Ex = ex;
			Ey = ey;
			LineWidth = lineWidth;

			DefaultPen.Width = LineWidth;
			DefaultPen.Color = Color.DarkGreen;

			ProfilePen.Width = LineWidth;
			ProfilePen.Color = Color.White;

			HolePen = new Pen(Color.Black, LineWidth);
		}

		public float Sx { get; private set; }

		public float Sy { get; private set; }

		public float Ex { get; private set; }

		public float Ey { get; private set; }

		public float LineWidth { get; private set; }

		public Pen HolePen { get; private set; }

		public override void Fill(Graphics graphics)
		{
			if (IsPositive)
			{
				graphics.DrawLine(DefaultPen, Sx, Sy, Ex, Ey);
			}
			else
			{
				graphics.DrawLine(HolePen, Sx, Sy, Ex, Ey);
			}
		}

		public override void Draw(Graphics graphics)
		{
			graphics.DrawLine(ProfilePen, Sx, Sy, Ex, Ey);
		}
	}
}
