using System;
using System.Drawing;
using CressemDataToGraphics.Converter;
using CressemExtractLibrary.Data.Interface.Features;
using ImageControl.Extension;
using ImageControl.Shape.Gdi.Interface;

namespace CressemDataToGraphics.Model.Graphics.Shape
{
	internal class ShapeArc : ShapeBase, IGdiArc
	{
		private ShapeArc() { }

		public ShapeArc(float pixelResolution,
			float x, float y,
			float width, float height,
			float startAngle, float sweepAngle,
			float lineWidth) : base(pixelResolution)
		{
			X = x;
			Y = y;
			Width = width;
			Height = height;
			StartAngle = startAngle;
			SweepAngle = sweepAngle;
			LineWidth = lineWidth;
		}

		public float X { get; private set; }

		public float Y { get; private set; }

		public float Width { get; private set; }

		public float Height { get; private set; }

		public float StartAngle { get; private set; }

		public float SweepAngle { get; private set; }

		public float LineWidth { get; private set; }

		public static ShapeArc CreateGdiPlus(bool useMM,
			float pixelResolution, bool isMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis,
			double sx, double sy, double ex, double ey, double arcCx, double arcCy,	
			bool isClockWise, double width)
		{
			float fsx = (float)sx;
			float fsy = (float)sy;
			float fex = (float)ex;
			float fey = (float)ey;
			float fAcx = (float)arcCx;
			float fAcy = (float)arcCy;
			float fwidth = (float)width;

			if (useMM is true)
			{
				if (isMM is false)
				{
					fsx = (float)sx.ConvertInchToMM();
					fsy = (float)sy.ConvertInchToMM();
					fex = (float)ex.ConvertInchToMM();
					fey = (float)ey.ConvertInchToMM();
					fAcx = (float)arcCx.ConvertInchToMM();
					fAcy = (float)arcCy.ConvertInchToMM();
					fwidth = (float)width.ConvertInchToUM();
				}
			}
			else
			{
				if (isMM is true)
				{
					fsx = (float)sx.ConvertMMToInch();
					fsy = (float)sy.ConvertMMToInch();
					fex = (float)ex.ConvertMMToInch();
					fey = (float)ey.ConvertMMToInch();
					fAcx = (float)arcCx.ConvertMMToInch();
					fAcy = (float)arcCy.ConvertMMToInch();
					fwidth = (float)width.ConvertUMToInch();
				}
			}

			PointF start = new PointF(fsx, fsy);
			PointF end = new PointF(fex, fey);
			PointF center = new PointF(fAcx, fAcy);

			if (orient > 0)
			{
				PointF datum = new PointF(
					(float)(xDatum + cx), (float)(yDatum + cy));

				start = start.Rotate(datum, orient, isMirrorXAxis);
				end = end.Rotate(datum, orient, isMirrorXAxis);
				center = center.Rotate(datum, orient, isMirrorXAxis);
			}

			double radius = Math.Sqrt(Math.Pow(start.X - center.X, 2) + Math.Pow(-(start.Y - center.Y), 2));
			double startAngle = Math.Atan2(-(sy - center.X), start.X - center.X) * (180 / Math.PI);
			double endAngle = Math.Atan2(-(end.Y - center.Y), end.X - center.X) * (180 / Math.PI);
			double sweepAngle = (endAngle - startAngle);

			if (isClockWise is true)
			{
				sweepAngle = sweepAngle <= 0 ?
					(float)sweepAngle + 360.0f : (float)sweepAngle;
			}
			else
			{
				sweepAngle = sweepAngle >= 0 ?
					(float)sweepAngle - 360.0f : (float)sweepAngle;
			}

			return new ShapeArc(pixelResolution,
				(float)(fAcx - radius),
				(float)-(fAcy + radius),
				(float)(radius * 2),
				(float)(radius * 2),
				(float)startAngle,
				(float)sweepAngle,
				(float)fwidth);
		}

		public static IGdiArc CreateOpenGl(float pixelResolution,
			IFeatureArc arc)
		{
			throw new NotImplementedException();
		}
	}
}
