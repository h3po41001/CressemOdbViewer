using System;
using CressemDataToGraphics.Factory;
using ImageControl.Shape.DirectX.Interface;

namespace CressemDataToGraphics.Model.Graphics.DirectX
{
	internal class ShapeDirectArc : ShapeDirectBase, IDirectArc
	{
		private ShapeDirectArc() : base() { }

		public ShapeDirectArc(float sx, float sy,
			float ex, float ey,
			float width, float height,
			float rotation, bool isLargeArc,
			bool isClockwise) : base()
		{
			Sx = sx;
			Sy = sy;
			Ex = ex;
			Ey = ey;
			Width = width;
			Height = height;
			Rotation = rotation;
			IsLargeArc = isLargeArc;
			IsClockwise = isClockwise;
		}
		public float Sx { get; private set; }

		public float Sy { get; private set; }

		public float Ex { get; private set; }

		public float Ey { get; private set; }

		public float Width { get; private set; }

		public float Height { get; private set; }

		public float Rotation { get; private set; }

		public bool IsLargeArc { get; private set; }

		public bool IsClockwise { get; private set; }

		public static ShapeDirectArc Create(bool useMM,
			float pixelResolution, bool isMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis,
			double sx, double sy, double ex, double ey, double arcCx, double arcCy,
			bool isClockwise, double width)
		{
			var shapeArc = ShapeFactory.Instance.CreateArcShape(useMM, pixelResolution, isMM,
				xDatum, yDatum, cx, cy, orient, isMirrorXAxis,
				sx, sy, ex, ey, arcCx, arcCy, width);

			double deltaAngle = shapeArc.EndAngle - shapeArc.StartAngle;
			if (deltaAngle < 0)
			{
				deltaAngle += 2 * Math.PI;
			}

			bool isLarge;
			if (deltaAngle <= Math.PI)
			{
				isLarge = false;
			}
			else
			{
				isLarge = true;
			}

			return new ShapeDirectArc(shapeArc.Sx, shapeArc.Sy,
				shapeArc.Ex, shapeArc.Ey,
				shapeArc.Radius, shapeArc.Radius, 0,
				isLarge, isClockwise);
		}
	}
}
