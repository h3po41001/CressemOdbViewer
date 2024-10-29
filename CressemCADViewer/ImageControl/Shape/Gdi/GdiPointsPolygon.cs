using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using ImageControl.Model.Shape.Gdi;
using ImageControl.Shape.Gdi.Interface;

namespace ImageControl.Shape.Gdi
{
	internal class GdiPointsPolygon : GdiShape
	{
		public GdiPointsPolygon(bool isPositive, 
			bool isFill,
			IEnumerable<PointF> points) : base(isPositive)
		{
			IsFill = isFill;
			Points = new List<PointF>(points);

			ProfilePen.Color = Color.White;
			HoleBrush = new SolidBrush(Color.Black);
		}

		public bool IsFill { get; private set; }

		public IEnumerable<PointF> Points { get; private set; }

		public SolidBrush HoleBrush { get; private set; } 

		public override void Fill(Graphics graphics)
		{
			if (IsFill)
			{
				graphics.FillPolygon(SolidBrush, Points.ToArray());
			}
			else
			{
				graphics.FillPolygon(HoleBrush, Points.ToArray());
			}
		}

		public override void Draw(Graphics graphics)
		{
			graphics.DrawPolygon(ProfilePen, Points.ToArray());
		}
	}
}
