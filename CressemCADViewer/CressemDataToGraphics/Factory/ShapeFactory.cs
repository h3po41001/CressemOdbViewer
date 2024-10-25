using System;
using System.Drawing;
using CressemDataToGraphics.Converter;
using CressemDataToGraphics.Model.Cad;
using ImageControl.Extension;

namespace CressemDataToGraphics.Factory
{
	internal class ShapeFactory
	{
		private static ShapeFactory _instance = null;

		private ShapeFactory() { }

		public static ShapeFactory Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new ShapeFactory();
				}
				return _instance;
			}
		}

		public ShapeArc CreateArcShape(bool useMM,
			float pixelResolution, bool isMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis,
			double sx, double sy, double ex, double ey,
			double arcCx, double arcCy, double width)
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

			double radius = Math.Sqrt(
				Math.Pow(start.X - center.X, 2) +
				Math.Pow(start.Y - center.Y, 2));
			double startAngle = Math.Atan2(start.Y - center.Y, start.X - center.X) * (180 / Math.PI);
			double endAngle = Math.Atan2(end.Y - center.Y, end.X - center.X) * (180 / Math.PI);

			return new ShapeArc(pixelResolution,
				start.X, start.Y, end.X, end.Y, center.X, center.Y,
				fwidth, (float)radius, (float)startAngle, (float)endAngle);
		}
	}
}
