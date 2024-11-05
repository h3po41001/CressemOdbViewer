using CressemDataToGraphics.Factory;
using ImageControl.Shape.DirectX.Interface;

namespace CressemDataToGraphics.Model.Graphics.DirectX
{
	internal class ShapeDirectLine : ShapeGraphicsBase, IDirectLine
	{
		private ShapeDirectLine() { }

		public ShapeDirectLine(
			float sx, float sy,
			float ex, float ey,
			float width = 0) : base()
		{
			Sx = sx;
			Sy = sy;
			Ex = ex;
			Ey = ey;
			LineWidth = width;
		}

		public float Sx { get; private set; }

		public float Sy { get; private set; }

		public float Ex { get; private set; }

		public float Ey { get; private set; }

		public float LineWidth { get; private set; }

		public static ShapeDirectLine Create(bool useMM, float pixelResolution,
			int globalOrient, bool isGlobalFlipHorizontal,
			bool isMM, double datumX, double datumY,
			double anchorX, double anchorY,
			double cx, double cy,
			int orient, bool isFlipHorizontal,
			double sx, double sy, double ex, double ey, double width)
		{
			var shapeLine = ShapeFactory.Instance.CreateLine(useMM, pixelResolution,
				globalOrient, isGlobalFlipHorizontal,
				isMM, datumX, datumY,
				anchorX, anchorY,
				cx, cy,
				orient, isFlipHorizontal,
				sx, sy, ex, ey, width);

			return new ShapeDirectLine(shapeLine.Sx, -shapeLine.Sy,
				shapeLine.Ex, -shapeLine.Ey, shapeLine.LineWidth);
		}
	}
}
