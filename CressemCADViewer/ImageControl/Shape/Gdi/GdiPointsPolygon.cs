using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using ImageControl.Model.Shape.Gdi;

namespace ImageControl.Shape.Gdi
{
	internal class GdiPointsPolygon : GdiShape
	{
		public GdiPointsPolygon(float pixelResolution, bool isFill,
			IEnumerable<PointF> points) : base(pixelResolution)
		{
			IsFill = isFill;
			Points = new List<PointF>(points);

			ProfilePen.Color = Color.White;
		}

		public bool IsFill { get; private set; }

		public List<PointF> Points { get; private set; }

		public override void Draw(Graphics graphics)
		{			
			graphics.FillPolygon(SolidBrush, Points.ToArray());
		}

		public override void DrawProfile(Graphics graphics)
		{
			graphics.DrawPolygon(ProfilePen, Points.ToArray());
		}
	}
}
