using System.Drawing;
using System.Reflection.Emit;
using CressemDataToGraphics.Converter;
using ImageControl.Extension;
using ImageControl.Shape.Interface;

namespace CressemDataToGraphics.Model.Graphics.Shape
{
	internal class ShapeRectangle : ShapeBase, IShapeRectangle
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

		public static ShapeRectangle CreateGdiPlus(bool useMM, float pixelResolution,
			double xDatum, double yDatum, int orient,
			bool isMM, double cx, double cy,
			double width, double height)
		{
			float sx = (float)cx;
			float sy = (float)cy;
			float fWidth = (float)width;
			float fHeight = (float)height;

			if (useMM is true)
			{
				if (isMM is false)
				{
					sx = (float)cx.ConvertInchToMM();
					sy = (float)cy.ConvertInchToMM();
					fWidth = (float)width.ConvertInchToUM();
					fHeight = (float)height.ConvertInchToUM();
				}
			}
			else
			{
				if (isMM is true)
				{
					sx = (float)cx.ConvertMMToInch();
					sy = (float)cy.ConvertMMToInch();
					fWidth = (float)width.ConvertUMToInch();
					fHeight = (float)height.ConvertUMToInch();
				}
			}

			PointF lt = new PointF(sx - fWidth / 2, sy + fHeight / 2);
			PointF rb = new PointF(sx + fWidth / 2, sy - fHeight / 2);

			if (orient > 0)
			{
				PointF datum = new PointF((float)(xDatum + sx), (float)(yDatum + sy));
				int orientAngle = (orient % 4) * 90;
				bool isMirrorXAxis = orient >= 4;

				lt = lt.Rotate(datum, orientAngle, isMirrorXAxis);
				rb = rb.Rotate(datum, orientAngle, isMirrorXAxis);

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
