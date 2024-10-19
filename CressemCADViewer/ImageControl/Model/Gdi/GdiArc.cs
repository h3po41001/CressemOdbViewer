using System.Drawing;

namespace ImageControl.Model.Gdi
{
	public class GdiArc : GdiShape
	{
		private GdiArc() { }

		public GdiArc(RectangleF boundary, float startAngle, float sweepAngle)
		{
			Boundary = boundary;
			StartAngle = startAngle;
			SweepAngle = sweepAngle;
		}

		public RectangleF Boundary { get; private set; }

		public float StartAngle { get; private set; }

		public float SweepAngle { get; private set; }
	}
}
