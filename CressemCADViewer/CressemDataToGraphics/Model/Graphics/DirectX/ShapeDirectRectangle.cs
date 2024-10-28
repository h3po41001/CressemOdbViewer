using CressemDataToGraphics.Factory;

namespace CressemDataToGraphics.Model.Graphics.DirectX
{
	internal class ShapeDirectRectangle : ShapeDirectBase
	{
		private ShapeDirectRectangle() : base()
		{
		}

		public ShapeDirectRectangle(float sx, float sy,
			float width, float height) : base()
		{
			Sx = sx;
			Sy = sy;
			Width = width;
			Height = height;
		}

		public float Sx { get; private set; }

		public float Sy { get; private set; }

		public float Width { get; private set; }

		public float Height { get; private set; }

		public static ShapeDirectRectangle Create(bool useMM,
			float pixelResolution, bool isMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis,
			double width, double height)
		{
			var shapeRectangle = ShapeFactory.Instance.CreateRectangle(useMM, pixelResolution, isMM,
				xDatum, yDatum, cx, cy, orient, isMirrorXAxis, width, height);

			return new ShapeDirectRectangle(shapeRectangle.Sx, -shapeRectangle.Sy,
				shapeRectangle.Width, shapeRectangle.Height);
		}
	}
}
