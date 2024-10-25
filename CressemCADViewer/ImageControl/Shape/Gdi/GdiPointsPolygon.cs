using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using ImageControl.Model.Shape.Gdi;
using ImageControl.Shape.Interface;

namespace ImageControl.Shape.Gdi
{
	internal class GdiPointsPolygon : GdiShape, IGdiPolygon
	{
		public GdiPointsPolygon(bool isFill,
			IEnumerable<PointF> points) : base()
		{
			IsFill = isFill;
			Points = new List<PointF>(points);

			ProfilePen.Color = Color.White;
		}

		public bool IsFill { get; private set; }

		public IEnumerable<PointF> Points { get; private set; }

		public IEnumerable<IGdiBase> Shapes { get; private set; }

		public override void Fill(Graphics graphics)
		{
			graphics.FillPolygon(SolidBrush, Points.ToArray());
		}

		public override void Draw(Graphics graphics)
		{
			graphics.DrawPolygon(ProfilePen, Points.ToArray());
		}
	}
}
