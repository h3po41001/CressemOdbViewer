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
			double datumX, double datumY,
			double cx, double cy,
			int orient, bool isFlipHorizontal,
			double sx, double sy, double ex, double ey,
			double arcCx, double arcCy, double width)
		{
			float fDatumX = (float)datumX;
			float fDatumY = (float)datumY;
			float fcx = (float)cx;
			float fcy = (float)cy;
			float fsx = (float)sx;
			float fsy = (float)sy;
			float fex = (float)ex;
			float fey = (float)ey;
			float fAcx = (float)arcCx;
			float fAcy = (float)arcCy;
			float fWidth = (float)width;

			if (useMM is true)
			{
				if (isMM is false)
				{
					fDatumX = (float)datumX.ConvertInchToMM();
					fDatumY = (float)datumY.ConvertInchToMM();
					fcx = (float)cx.ConvertInchToMM();
					fcy = (float)cy.ConvertInchToMM();
					fsx = (float)sx.ConvertInchToMM();
					fsy = (float)sy.ConvertInchToMM();
					fex = (float)ex.ConvertInchToMM();
					fey = (float)ey.ConvertInchToMM();
					fAcx = (float)arcCx.ConvertInchToMM();
					fAcy = (float)arcCy.ConvertInchToMM();
					fWidth = (float)width.ConvertInchToUM();
				}
			}
			else
			{
				if (isMM is true)
				{
					fDatumX = (float)datumX.ConvertMMToInch();
					fDatumY = (float)datumY.ConvertMMToInch();
					fcx = (float)cx.ConvertMMToInch();
					fcy = (float)cy.ConvertMMToInch();
					fsx = (float)sx.ConvertMMToInch();
					fsy = (float)sy.ConvertMMToInch();
					fex = (float)ex.ConvertMMToInch();
					fey = (float)ey.ConvertMMToInch();
					fAcx = (float)arcCx.ConvertMMToInch();
					fAcy = (float)arcCy.ConvertMMToInch();
					fWidth = (float)width.ConvertUMToInch();
				}
			}

			PointF start = new PointF(fsx, fsy);
			start = start.Offset(fDatumX, fDatumY);

			PointF end = new PointF(fex, fey);
			end = end.Offset(fDatumX, fDatumY);

			PointF center = new PointF(fAcx, fAcy);
			center = center.Offset(fDatumX, fDatumY);

			PointF datum = new PointF(fcx, fcy);
			datum = datum.Offset(fDatumX, fDatumY);

			// 좌표계가 수학적 좌표계 이기 때문에 -곱해야 함
			start = start.Rotate(datum, -orient, isFlipHorizontal);
			end = end.Rotate(datum, -orient, isFlipHorizontal);
			center = center.Rotate(datum, -orient, isFlipHorizontal);

			double radius = Math.Sqrt(
				Math.Pow(start.X - center.X, 2) +
				Math.Pow(start.Y - center.Y, 2));
			double startAngle = Math.Atan2(start.Y - center.Y, start.X - center.X) * (180 / Math.PI);
			double endAngle = Math.Atan2(end.Y - center.Y, end.X - center.X) * (180 / Math.PI);

			return new ShapeArc(pixelResolution,
				fcx, fcy, start.X, start.Y, end.X, end.Y, center.X, center.Y,
				fWidth, (float)radius, (float)startAngle, (float)endAngle);
		}

		public ShapeEllipse CreateEllipse(bool useMM,
			float pixelResolution, bool isMM,
			double datumX, double datumY,
			double cx, double cy,
			int orient, bool isFlipHorizontal,
			double width, double height)
		{
			float fDatumX = (float)datumX;
			float fDatumY = (float)datumY;
			float fcx = (float)cx;
			float fcy = (float)cy;
			float fWidth = (float)width;
			float fHeight = (float)height;

			if (useMM is true)
			{
				if (isMM is false)
				{
					fDatumX = (float)fDatumX.ConvertInchToMM();
					fDatumY = (float)fDatumY.ConvertInchToMM();
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
					fDatumX = (float)datumX.ConvertMMToInch();
					fDatumY = (float)datumY.ConvertMMToInch();
					fcx = (float)cx.ConvertMMToInch();
					fcy = (float)cy.ConvertMMToInch();
					fWidth = (float)width.ConvertUMToInch();
					fHeight = (float)height.ConvertUMToInch();
				}
			}

			PointF lt = new PointF(fcx - fWidth / 2, fcy + fHeight / 2);
			lt = lt.Offset(fDatumX, fDatumY);

			PointF rb = new PointF(fcx + fWidth / 2, fcy - fHeight / 2);
			rb = rb.Offset(fDatumX, fDatumY);

			PointF datum = new PointF(fcx, fcy);
			datum = datum.Offset(fDatumX, fDatumY);

			// 좌표계가 수학적 좌표계 이기 때문에 -곱해야 함
			lt = lt.Rotate(datum, -orient, isFlipHorizontal);
			rb = rb.Rotate(datum, -orient, isFlipHorizontal);

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

			return new ShapeEllipse(pixelResolution, fcx, fcy,
				lt.X, lt.Y, fWidth, fHeight);
		}

		public ShapeLine CreateLine(bool useMM,
			float pixelResolution, bool isMM,
			double datumX, double datumY,
			double cx, double cy,
			int orient, bool isFlipHorizontal,
			double sx, double sy, double ex, double ey, double width)
		{
			float fDatumX = (float)datumX;
			float fDatumY = (float)datumY;
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
					fDatumX = (float)datumX.ConvertInchToMM();
					fDatumY = (float)datumY.ConvertInchToMM();
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
					fDatumX = (float)datumX.ConvertMMToInch();
					fDatumY = (float)datumY.ConvertMMToInch();
					fcx = (float)cx.ConvertMMToInch();
					fcy = (float)cy.ConvertMMToInch();
					fsx = (float)sx.ConvertMMToInch();
					fsy = (float)sy.ConvertMMToInch();
					fex = (float)ex.ConvertMMToInch();
					fey = (float)ey.ConvertMMToInch();
					fWidth = (float)width.ConvertUMToInch();
				}
			}

			// 좌표계가 수학적 좌표계 이기 때문에 -곱해야 함
			PointF anchor = new PointF(fcx, fcy);
			//anchor = anchor.Offset(fDatumX, fDatumY);

			PointF start = new PointF(fsx, fsy);
			start = start.Offset(fDatumX, fDatumY);

			PointF end = new PointF(fex, fey);			
			end = end.Offset(fDatumX, fDatumY);

			start = start.Rotate(anchor, -orient, isFlipHorizontal);
			end = end.Rotate(anchor, -orient, isFlipHorizontal);

			return new ShapeLine(pixelResolution, fcx, fcy,
				start.X, start.Y, end.X, end.Y, fWidth);
		}

		public ShapeRectangle CreateRectangle(bool useMM,
			float pixelResolution, bool isMM,
			double datumX, double datumY,
			double cx, double cy,
			int orient, bool isFlipHorizontal,
			double width, double height)
		{
			float fDatumX = (float)datumX;
			float fDatumY = (float)datumY;
			float fcx = (float)cx;
			float fcy = (float)cy;
			float fWidth = (float)width;
			float fHeight = (float)height;

			if (useMM is true)
			{
				if (isMM is false)
				{
					fDatumX = (float)datumX.ConvertInchToMM();
					fDatumY = (float)datumY.ConvertInchToMM();
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
					fcx = (float)cx.ConvertMMToInch();
					fcy = (float)cy.ConvertMMToInch();
					fWidth = (float)width.ConvertUMToInch();
					fHeight = (float)height.ConvertUMToInch();
				}
			}

			PointF lt = new PointF(fcx - fWidth / 2, fcy + fHeight / 2);
			lt = lt.Offset(fDatumX, fDatumY);

			PointF rb = new PointF(fcx + fWidth / 2, fcy - fHeight / 2);
			rb = rb.Offset(fDatumX, fDatumY);

			PointF datum = new PointF(fcx, fcy);
			datum = datum.Offset(fDatumX, fDatumY);

			// 좌표계가 수학적 좌표계 이기 때문에 -곱해야 함
			lt = lt.Rotate(datum, -orient, isFlipHorizontal);
			rb = rb.Rotate(datum, -orient, isFlipHorizontal);

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

			return new ShapeRectangle(pixelResolution, fcx, fcy, lt.X, lt.Y, fWidth, fHeight);
		}

		public ShapePolygon CreatePolygon(bool useMM,
			float pixelResolution, bool isMM,
			double datumX, double datumY,
			double cx, double cy,
			int orient, bool isFlipHorizontal,
			bool isPositive, string polygonType,
			IEnumerable<PointF> points)
		{
			float fDatumX = (float)datumX;
			float fDatumY = (float)datumY;
			float fcx = (float)cx;
			float fcy = (float)cy;
			List<PointF> fPoints = new List<PointF>();

			if (useMM is true)
			{
				if (isMM is false)
				{
					fDatumX = (float)datumX.ConvertInchToMM();
					fDatumY = (float)datumY.ConvertInchToMM();
					fcx = (float)cx.ConvertInchToMM();
					fcy = (float)cy.ConvertInchToMM();
					fPoints = new List<PointF>(points.ConvertInchToMM());
				}
			}
			else
			{
				if (isMM is true)
				{
					fDatumX = (float)fDatumX.ConvertMMToInch();
					fDatumY = (float)fDatumY.ConvertMMToInch();
					fcx = (float)cx.ConvertMMToInch();
					fcy = (float)cy.ConvertMMToInch();
					fPoints = new List<PointF>(points.ConvertMMToInch());
				}
			}

			List<PointF> calcPoints = new List<PointF>();

			PointF datum = new PointF(fcx, fcy);
			datum = datum.Offset(fDatumX, fDatumY);

			foreach (var point in fPoints)
			{
				var calcPoint = point.Offset(fDatumX, fDatumY);

				// 좌표계가 수학적 좌표계 이기 때문에 -곱해야 함
				calcPoint = calcPoint.Rotate(datum, -orient, isFlipHorizontal);
				calcPoints.Add(calcPoint);
			}

			bool isIsland = polygonType.Equals("I") is true;
			bool isFill = isPositive is true ? isIsland : !isIsland;

			return new ShapePolygon(pixelResolution, fcx, fcy, isFill, calcPoints);
		}
	}
}
