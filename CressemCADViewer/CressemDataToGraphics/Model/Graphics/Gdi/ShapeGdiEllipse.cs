using System.Drawing;
using CressemDataToGraphics.Converter;
using CressemDataToGraphics.Factory;
using ImageControl.Extension;
using ImageControl.Shape.Gdi.Interface;

namespace CressemDataToGraphics.Model.Graphics.Shape
{
	internal class ShapeGdiEllipse : ShapeGraphicsBase, IGdiEllipse
	{
		private ShapeGdiEllipse() : base()
		{
		}

		public ShapeGdiEllipse(float sx, float sy,
			float width, float height) : base()
		{
			Sx = sx;
			Sy = sy;
			Width = width;
			Height = height;
		}

		public float Sx { get; set; }

		public float Sy { get; set; }

		public float Width { get; set; }

		public float Height { get; set; }

		public static ShapeGdiEllipse Create(bool useMM,
			float pixelResolution, bool isMM,
			double globalDatumX, double globalDatumY,
			double localDatumX, double localDatumY,
			double cx, double cy,
			int orient, bool isFlipHorizontal,
			double width, double height)
		{
			var shapeEllipse = ShapeFactory.Instance.CreateEllipse(useMM, 
				pixelResolution, isMM,
				globalDatumX, globalDatumY,
				localDatumX, localDatumY, cx, cy, 
				orient, isFlipHorizontal, 
				width, height);

			// Graphics는 y좌표가 반대이므로 -1곱한다
			return new ShapeGdiEllipse(shapeEllipse.Sx, -shapeEllipse.Sy,
				shapeEllipse.Width, shapeEllipse.Height);
		}
	}
}
