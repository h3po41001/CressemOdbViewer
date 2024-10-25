using System;
using CressemDataToGraphics.Factory;
using CressemExtractLibrary.Data.Interface.Features;
using ImageControl.Shape.Gdi.Interface;

namespace CressemDataToGraphics.Model.Graphics.Shape
{
	internal class ShapeGdiArc : ShapeGdiBase, IGdiArc
	{
		private ShapeGdiArc() { }

		public ShapeGdiArc(float pixelResolution,
			float x, float y,
			float width, float height,
			float startAngle, float sweepAngle,
			float lineWidth) : base(pixelResolution)
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

		public static ShapeGdiArc Create(bool useMM,
			float pixelResolution, bool isMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis,
			double sx, double sy, double ex, double ey, double arcCx, double arcCy,
			bool isClockWise, double width)
		{
			var shapeArc = ShapeFactory.Instance.CreateArcShape(useMM, pixelResolution, isMM,
				xDatum, yDatum, cx, cy, orient, isMirrorXAxis,
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

			return new ShapeGdiArc(pixelResolution,
				(float)(shapeArc.ShapeCx - shapeArc.Radius),
				(float)-(shapeArc.ShapeCy + shapeArc.Radius),
				(float)(shapeArc.Radius * 2),
				(float)(shapeArc.Radius * 2),
				(float)shapeArc.StartAngle,
				(float)sweepAngle,
				shapeArc.Width);
		}

		public static IGdiArc CreateOpenGl(float pixelResolution,
			IFeatureArc arc)
		{
			throw new NotImplementedException();
		}
	}
}
