using CressemDataToGraphics.Converter;
using CressemDataToGraphics.Factory;
using ImageControl.Shape.DirectX.Interface;

namespace CressemDataToGraphics.Model.Graphics.DirectX
{
	internal class ShapeDirectArc : ShapeGraphicsBase, IDirectArc
	{
		private ShapeDirectArc() : base() { }

		public ShapeDirectArc(float sx, float sy,
			float ex, float ey,
			float radiusWidth, float radiusHeight,
			float rotation, bool isLargeArc,
			bool isClockwise, float lineWidth) : base()
		{
			Sx = sx;
			Sy = sy;
			Ex = ex;
			Ey = ey;
			RadiusWidth = radiusWidth;
			RadiusHeight = radiusHeight;
			Rotation = rotation;
			IsLargeArc = isLargeArc;
			IsClockwise = isClockwise;
			LineWidth = lineWidth;
		}

		public float Sx { get; private set; }

		public float Sy { get; private set; }

		public float Ex { get; private set; }

		public float Ey { get; private set; }

		public float RadiusWidth { get; private set; }

		public float RadiusHeight { get; private set; }

		public float Rotation { get; private set; }

		public bool IsLargeArc { get; private set; }

		public bool IsClockwise { get; private set; }

		public float LineWidth { get; private set; }

		public static ShapeDirectArc Create(bool useMM,
			float pixelResolution, bool isMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis,
			double sx, double sy, double ex, double ey, double arcCx, double arcCy,
			bool isClockwise, double width)
		{
			var shapeArc = ShapeFactory.Instance.CreateArc(useMM, pixelResolution, isMM,
				xDatum, yDatum, cx, cy, orient, isMirrorXAxis,
				sx, sy, ex, ey, arcCx, arcCy, width);

			double startAngle = DataConverter.ConvertNormalizeAngle(shapeArc.StartAngle);
			double endAngle = DataConverter.ConvertNormalizeAngle(shapeArc.EndAngle);

			double deltaAngle = endAngle - startAngle;
			if (deltaAngle <= 0)
			{
				deltaAngle += 360;
			}

			bool isLarge = (deltaAngle > 180) is true ? !isClockwise : isClockwise;

			return new ShapeDirectArc(
				shapeArc.Sx, -shapeArc.Sy,
				shapeArc.Ex, -shapeArc.Ey,
				shapeArc.Radius, shapeArc.Radius, 0,
				isLarge, isClockwise, shapeArc.Width);
		}
	}
}
