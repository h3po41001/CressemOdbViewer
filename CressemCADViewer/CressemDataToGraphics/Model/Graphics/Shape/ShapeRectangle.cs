using System.Drawing;
using CressemDataToGraphics.Converter;
using ImageControl.Extension;
using ImageControl.Shape.Gdi.Interface;

namespace CressemDataToGraphics.Model.Graphics.Shape
{
	internal class ShapeRectangle : ShapeBase, IGdiRectangle
	{
		private ShapeRectangle() : base()
		{
		}

		public ShapeRectangle(float pixelResolution,
			float x, float y,
			float width, float height) : base(pixelResolution)
		{
			X = x;
			Y = y;
			Width = width;
			Height = height;
		}

		public float X { get; set; }

		public float Y { get; set; }

		public float Width { get; set; }

		public float Height { get; set; }

		public static ShapeRectangle CreateGdiPlus(bool useMM,
			float pixelResolution, bool isMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis,
			double width, double height)
		{
			float fcx = (float)cx;
			float fcy = (float)cy;
			float fWidth = (float)width;
			float fHeight = (float)height;

			if (useMM is true)
			{
				if (isMM is false)
				{
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
			PointF rb = new PointF(fcx + fWidth / 2, fcy - fHeight / 2);

			if (orient > 0)
			{
				PointF datum = new PointF((float)(xDatum + fcx), (float)(yDatum + fcy));

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

			// Gdi에 그릴때는 LT부터 width, height 만큼 그림
			// ODB에서 LT 좌표는 (sx - fWidth / 2), (sy + fHeight / 2) 
			// 하지만 Gdi는 y좌표가 반대이므로 -1곱한다
			return new ShapeRectangle(pixelResolution,
				lt.X, -lt.Y, fWidth, fHeight);
		}
	}
}
