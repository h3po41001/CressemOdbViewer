using CressemDataToGraphics.Factory;
using ImageControl.Shape.DirectX.Interface;

namespace CressemDataToGraphics.Model.Graphics.DirectX
{
	internal class ShapeDirectEllipse : ShapeGraphicsBase, IDirectEllipse
	{
		private ShapeDirectEllipse() : base() { }

		public ShapeDirectEllipse(float cx, float cy,
			float radiusX, float radiusY) : base()
		{
			Cx = cx;
			Cy = cy;
			RadiusX = radiusX;
			RadiusY = radiusY;
		}

		public float Cx { get; private set; }

		public float Cy { get; private set; }

		public float RadiusX { get; private set; }

		public float RadiusY { get; private set; }

		public static ShapeDirectEllipse Create(bool useMM, float pixelResolution,
			int globalOrient, bool isGlobalFlipHorizontal, bool isMM,
			double datumX, double datumY,
			double cx, double cy,
			int orient, bool isFlipHorizontal,
			double width, double height)
		{
			var shapeEllipse = ShapeFactory.Instance.CreateEllipse(useMM, pixelResolution,
				globalOrient, isGlobalFlipHorizontal,
				isMM, datumX, datumY,
				 cx, cy,
				orient, isFlipHorizontal, width, height);

			// Graphics는 y좌표가 반대이므로 -1곱한다
			return new ShapeDirectEllipse(shapeEllipse.ShapeCx, -shapeEllipse.ShapeCy,
				shapeEllipse.Width / 2, shapeEllipse.Height / 2);
		}
	}
}
