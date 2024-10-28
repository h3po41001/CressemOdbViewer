using System;
using System.Collections.Generic;
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

		public ShapeArc CreateArc(bool useMM,
			float pixelResolution, bool isMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis,
			double sx, double sy, double ex, double ey,
			double arcCx, double arcCy, double width)
		{
			float fDatumX = (float)xDatum;
			float fDatumY = (float)yDatum;
			float fcx = (float)cx;
			float fcy = (float)cy;
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
					fDatumX = (float)xDatum.ConvertInchToMM();
					fDatumY = (float)yDatum.ConvertInchToMM();
					fcx = (float)cx.ConvertInchToMM();
					fcy = (float)cy.ConvertInchToMM();
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
					fDatumX = (float)xDatum.ConvertMMToInch();
					fDatumY = (float)yDatum.ConvertMMToInch();
					fcx = (float)cx.ConvertMMToInch();
					fcy = (float)cy.ConvertMMToInch();
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
					(float)(fDatumX + fcx), (float)(fDatumY + fcy));

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

		public ShapeEllipse CreateEllipse(bool useMM,
			float pixelResolution, bool isMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis,
			double width, double height)
		{
			float fDatumX = (float)xDatum;
			float fDatumY = (float)yDatum;
			float fcx = (float)cx;
			float fcy = (float)cy;
			float fWidth = (float)width;
			float fHeight = (float)height;

			if (useMM is true)
			{
				if (isMM is false)
				{
					fDatumX = (float)xDatum.ConvertInchToMM();
					fDatumY = (float)yDatum.ConvertInchToMM();
					fcx = (float)cx.ConvertInchToMM();
					fcy = (float)cy.ConvertInchToMM();
					fWidth = (float)width.ConvertInchToUM();
					fHeight = (float)height.ConvertInchToUM();
				}
			}
			else
			{
				if (isMM is true)
				{
					fDatumX = (float)xDatum.ConvertMMToInch();
					fDatumY = (float)yDatum.ConvertMMToInch();
					fcx = (float)cx.ConvertMMToInch();
					fcy = (float)cy.ConvertMMToInch();
					fWidth = (float)width.ConvertUMToInch();
					fHeight = (float)height.ConvertUMToInch();
				}
			}

			PointF lt = new PointF(fcx - fWidth / 2, fcy + fHeight / 2);
			PointF rb = new PointF(fcx + fWidth / 2, fcy - fHeight / 2);

			if (orient > 0)
			{
				PointF datum = new PointF((float)(fDatumX + fcx), (float)(fDatumY + fcy));

				lt = lt.Rotate(datum, orient, isMirrorXAxis);
				rb = rb.Rotate(datum, orient, isMirrorXAxis);

				float left = lt.X;
				float right = rb.X;
				float top = lt.Y;
				float bottom = rb.Y;

				if (lt.X > rb.X)
				{
					left = rb.X;
					right = lt.X;
				}

				if (rb.Y > lt.Y)
				{
					top = rb.Y;
					bottom = lt.Y;
				}

				lt = new PointF(left, top);
				rb = new PointF(right, bottom);

				fWidth = rb.X - lt.X;
				fHeight = lt.Y - rb.Y;
			}

			return new ShapeEllipse(pixelResolution, fcx, fcy,
				lt.X, lt.Y, fWidth, fHeight);
		}

		public ShapeLine CreateLine(bool useMM,
			float pixelResolution, bool isMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis,
			double sx, double sy, double ex, double ey, double width)
		{
			float fDatumX = (float)xDatum;
			float fDatumY = (float)yDatum;
			float fcx = (float)cx;
			float fcy = (float)cy;

			float fsx = (float)sx;
			float fsy = (float)sy;
			float fex = (float)ex;
			float fey = (float)ey;
			float fWidth = (float)width;

			if (useMM is true)
			{
				if (isMM is false)
				{
					fDatumX = (float)xDatum.ConvertInchToMM();
					fDatumY = (float)yDatum.ConvertInchToMM();
					fcx = (float)cx.ConvertInchToMM();
					fcy = (float)cy.ConvertInchToMM();
					fsx = (float)sx.ConvertInchToMM();
					fsy = (float)sy.ConvertInchToMM();
					fex = (float)ex.ConvertInchToMM();
					fey = (float)ey.ConvertInchToMM();
					fWidth = (float)width.ConvertInchToUM();
				}
			}
			else
			{
				if (isMM is true)
				{
					fDatumX = (float)xDatum.ConvertMMToInch();
					fDatumY = (float)yDatum.ConvertMMToInch();
					fcx = (float)cx.ConvertMMToInch();
					fcy = (float)cy.ConvertMMToInch();
					fsx = (float)sx.ConvertMMToInch();
					fsy = (float)sy.ConvertMMToInch();
					fex = (float)ex.ConvertMMToInch();
					fey = (float)ey.ConvertMMToInch();
					fWidth = (float)width.ConvertUMToInch();
				}
			}

			PointF start = new PointF(fsx, fsy);
			PointF end = new PointF(fex, fey);

			if (orient > 0)
			{
				PointF datum = new PointF((float)(fDatumX + fcx), (float)(fDatumY + fcy));

				start = start.Rotate(datum, orient, isMirrorXAxis);
				end = end.Rotate(datum, orient, isMirrorXAxis);
			}

			return new ShapeLine(pixelResolution, fcx, fcy,
				start.X, start.Y, end.X, end.Y, fWidth);
		}

		public ShapeRectangle CreateRectangle(bool useMM,
			float pixelResolution, bool isMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis,
			double width, double height)
		{
			float fDatumX = (float)xDatum;
			float fDatumY = (float)yDatum;
			float fcx = (float)cx;
			float fcy = (float)cy;
			float fWidth = (float)width;
			float fHeight = (float)height;

			if (useMM is true)
			{
				if (isMM is false)
				{
					fDatumX = (float)xDatum.ConvertInchToMM();
					fDatumY = (float)yDatum.ConvertInchToMM();
					fcx = (float)cx.ConvertInchToMM();
					fcy = (float)cy.ConvertInchToMM();
					fWidth = (float)width.ConvertInchToUM();
					fHeight = (float)height.ConvertInchToUM();
				}
			}
			else
			{
				if (isMM is true)
				{
					fDatumX = (float)xDatum.ConvertMMToInch();
					fDatumY = (float)yDatum.ConvertMMToInch();
					fcx = (float)cx.ConvertMMToInch();
					fcy = (float)cy.ConvertMMToInch();
					fWidth = (float)width.ConvertUMToInch();
					fHeight = (float)height.ConvertUMToInch();
				}
			}

			PointF lt = new PointF(fcx - fWidth / 2, fcy + fHeight / 2);
			PointF rb = new PointF(fcx + fWidth / 2, fcy - fHeight / 2);

			if (orient > 0)
			{
				PointF datum = new PointF((float)(fDatumX + fcx), (float)(fDatumY + fcy));

				lt = lt.Rotate(datum, orient, isMirrorXAxis);
				rb = rb.Rotate(datum, orient, isMirrorXAxis);

				float left = lt.X;
				float right = rb.X;
				float top = lt.Y;
				float bottom = rb.Y;

				if (lt.X > rb.X)
				{
					left = rb.X;
					right = lt.X;
				}

				if (rb.Y > lt.Y)
				{
					top = rb.Y;
					bottom = lt.Y;
				}

				lt = new PointF(left, top);
				rb = new PointF(right, bottom);

				fWidth = rb.X - lt.X;
				fHeight = lt.Y - rb.Y;
			}

			return new ShapeRectangle(pixelResolution, fcx, fcy, lt.X, lt.Y, fWidth, fHeight);
		}

		public ShapePolygon CreatePolygon(bool useMM,
			float pixelResolution, bool isMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis,
			bool isPositive, string polygonType,
			IEnumerable<PointF> points)
		{
			float fDatumX = (float)xDatum;
			float fDatumY = (float)yDatum;
			float fcx = (float)cx;
			float fcy = (float)cy;
			List<PointF> fPoints = new List<PointF>();

			if (useMM is true)
			{
				if (isMM is false)
				{
					fDatumX = (float)xDatum.ConvertInchToMM();
					fDatumY = (float)yDatum.ConvertInchToMM();
					fcx = (float)cx.ConvertInchToMM();
					fcy = (float)cy.ConvertInchToMM();
					fPoints = new List<PointF>(points.ConvertInchToMM());
				}
			}
			else
			{
				if (isMM is true)
				{
					fDatumX = (float)xDatum.ConvertMMToInch();
					fDatumY = (float)yDatum.ConvertMMToInch();
					fcx = (float)cx.ConvertMMToInch();
					fcy = (float)cy.ConvertMMToInch();
					fPoints = new List<PointF>(points.ConvertMMToInch());
				}
			}

			List<PointF> calcPoints = new List<PointF>();

			PointF datum = new PointF(fDatumX + fcx, fDatumY + fcy);
			if (orient > 0)
			{
				foreach (var point in fPoints)
				{
					calcPoints.Add(point.Rotate(datum, orient, isMirrorXAxis));
				}
			}

			bool isIsland = polygonType.Equals("I") is true;
			bool isFill = isPositive is true ? isIsland : !isIsland;

			return new ShapePolygon(pixelResolution, fcx, fcy, isFill, calcPoints);
		}
	}
}
