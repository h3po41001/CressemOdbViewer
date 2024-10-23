using CressemDataToGraphics.Converter;
using ImageControl.Shape.Interface;

namespace CressemDataToGraphics.Model.Graphics.Shape
{
	internal class ShapeRectangle : ShapeBase, IShapeRectangle
	{
		private ShapeRectangle() : base()
		{
		}

		public ShapeRectangle(float pixelResolution,
			float xDatum, float yDatum, int orient,
			float x, float y,
			float width, float height) : base(pixelResolution, xDatum, yDatum, orient)
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
			bool isMM, double xDatum, double yDatum, double cx, double cy,
			int orient, double width, double height)
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

			// Gdi에 그릴때는 LT부터 width, height 만큼 그림
			// ODB에서 LT 좌표는 (sx - fWidth / 2), (sy + fHeight / 2) 
			// 하지만 Gdi는 y좌표가 반대이므로 -1곱한다
			return new ShapeRectangle(pixelResolution, (float)xDatum, (float)yDatum, orient,
				(sx - fWidth / 2), -(sy + fHeight / 2), fWidth, fHeight);
		}
	}
}
