using CressemDataToGraphics.Factory;
using ImageControl.Shape.Gdi.Interface;

namespace CressemDataToGraphics.Model.Graphics.Shape
{
	internal class ShapeGdiRectangle : ShapeGdiBase, IGdiRectangle
	{
		private ShapeGdiRectangle() : base()
		{
		}

		public ShapeGdiRectangle(float x, float y,
			float width, float height) : base()
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

		public static ShapeGdiRectangle Create(bool useMM,
			float pixelResolution, bool isMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis,
			double width, double height)
		{
			var shapeRectangle = ShapeFactory.Instance.CreateRectangle(useMM, pixelResolution, isMM,
				xDatum, yDatum, cx, cy, orient, isMirrorXAxis, width, height);

			// Grapchis는 y좌표가 반대이므로 -1곱한다
			return new ShapeGdiRectangle(shapeRectangle.Sx, -shapeRectangle.Sy, 
				shapeRectangle.Width, shapeRectangle.Height);
		}
	}
}
