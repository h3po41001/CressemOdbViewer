using CressemDataToGraphics.Factory;
using ImageControl.Shape.Gdi.Interface;

namespace CressemDataToGraphics.Model.Graphics.Shape
{
	internal class ShapeGdiLine : ShapeGraphicsBase, IGdiLine
	{
		private ShapeGdiLine() { }

		public ShapeGdiLine(float sx, float sy,
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

		public static ShapeGdiLine Create(bool useMM, float pixelResolution,
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

			return new ShapeGdiLine(shapeLine.Sx, -shapeLine.Sy,
				shapeLine.Ex, -shapeLine.Ey, shapeLine.LineWidth);
		}
	}
}
