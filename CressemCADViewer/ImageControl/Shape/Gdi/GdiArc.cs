using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using ImageControl.Shape.Interface;

namespace ImageControl.Model.Shape.Gdi
{
	internal class GdiArc : GdiShape
	{
		private GdiArc() { }

		public GdiArc(IShapeArc shape) : base(shape.PixelResolution)
		{
			double radius = Math.Sqrt(Math.Pow(shape.Sx - shape.Cx, 2) + Math.Pow(-(shape.Sy - shape.Cy), 2));
			double startAngle = Math.Atan2(-(shape.Sy - shape.Cy), shape.Sx - shape.Cx) * (180 / Math.PI);
			double endAngle = Math.Atan2(-(shape.Ey - shape.Cy), shape.Ex - shape.Cx) * (180 / Math.PI);
			double sweepAngle = (endAngle - startAngle);

			X = (float)(shape.Cx - radius);
			Y = (float)(-shape.Cy - radius);
			Width = (float)(radius * 2);
			Height = (float)(radius * 2);
			StartAngle = (float)startAngle;

			if (shape.IsClockWise)
			{
				SweepAngle = sweepAngle <= 0 ? 
					(float)sweepAngle + 360.0f : (float)sweepAngle;
			}
			else
			{
				SweepAngle = sweepAngle >= 0 ?
					(float)sweepAngle - 360.0f : (float)sweepAngle;
			}
		}

		public float X { get; private set; }

		public float Y { get; private set; }

		public float Width { get; private set; }

		public float Height { get; private set; }

		public float StartAngle { get; private set; }

		public float SweepAngle { get; private set; }

		public override void Draw(Graphics graphics)
		{
			graphics.DrawArc(DefaultPen,
				X * PixelResolution, Y * PixelResolution,
				Width * PixelResolution, Height * PixelResolution,
				StartAngle, SweepAngle);
		}
	}
}
