using CressemDataToGraphics.Factory;
using ImageControl.Shape.Gdi.Interface;

namespace CressemDataToGraphics.Model.Graphics.Shape
{
	internal class ShapeGdiArc : ShapeGraphicsBase, IGdiArc
	{
		private ShapeGdiArc() { }

		public ShapeGdiArc(float x, float y,
			float width, float height,
			float startAngle, float sweepAngle,
			float lineWidth) : base()
		{
			X = x;
			Y = y;
			Width = width;
			Height = height;
			StartAngle = startAngle;
			SweepAngle = sweepAngle;
			LineWidth = lineWidth;
		}

		public float X { get; private set; }

		public float Y { get; private set; }

		public float Width { get; private set; }

		public float Height { get; private set; }

		public float StartAngle { get; private set; }

		public float SweepAngle { get; private set; }

		public float LineWidth { get; private set; }

		public static ShapeGdiArc Create(bool useMM, float pixelResolution,
			int globalOrient, bool isGlobalFlipHorizontal, bool isMM,
			double datumX, double datumY,
			double anchorX, double anchorY,
			double cx, double cy,
			int orient, bool isFlipHorizontal,
			double sx, double sy, double ex, double ey, double arcCx, double arcCy,
			bool isClockWise, double width)
		{
			var shapeArc = ShapeFactory.Instance.CreateArc(useMM, pixelResolution,
				globalOrient, isGlobalFlipHorizontal,
				isMM, datumX, datumY,
				anchorX, anchorY,
				cx, cy,
				orient, isFlipHorizontal,
				sx, sy, ex, ey, arcCx, arcCy, width);

			double sweepAngle = (shapeArc.EndAngle - shapeArc.StartAngle);

			if (isClockWise is true)
			{
				sweepAngle = sweepAngle <= 0 ?
					(float)sweepAngle + 360.0f : (float)sweepAngle;
			}
			else
			{
				sweepAngle = sweepAngle >= 0 ?
					(float)sweepAngle - 360.0f : (float)sweepAngle;
			}

			return new ShapeGdiArc((float)(shapeArc.ShapeCx - shapeArc.Radius),
				(float)-(shapeArc.ShapeCy + shapeArc.Radius),
				(float)(shapeArc.Radius * 2),
				(float)(shapeArc.Radius * 2),
				(float)shapeArc.StartAngle,
				(float)sweepAngle,
				shapeArc.Width);
		}
	}
}
