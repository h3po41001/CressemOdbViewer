using System;
using CressemDataToGraphics.Converter;
using CressemExtractLibrary.Data.Interface.Features;
using ImageControl.Shape.Interface;

namespace CressemDataToGraphics.Model.Graphics.Shape
{
	internal class ShapeArc : ShapeBase, IShapeArc
	{
		private ShapeArc() { }

		public ShapeArc(float pixelResolution,			
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

		public static ShapeArc CreateGdiPlus(bool useMM, float pixelResolution,
			double width, IFeatureArc arc)
		{
			double sx = arc.X;
			double sy = arc.Y;
			double ex = arc.Ex;
			double ey = arc.Ey;
			double cx = arc.Cx;
			double cy = arc.Cy;
			double lineWidth = width;

			if (useMM is true)
			{
				if (arc.IsMM is false)
				{
					sx = sx.ConvertInchToMM();
					sy = sy.ConvertInchToMM();
					ex = ex.ConvertInchToMM();
					ey = ey.ConvertInchToMM();
					cx = cx.ConvertInchToMM();
					cy = cy.ConvertInchToMM();
					lineWidth = lineWidth.ConvertInchToUM();
				}
			}
			else
			{
				if (arc.IsMM is true)
				{
					sx = sx.ConvertMMToInch();
					sy = sy.ConvertMMToInch();
					ex = ex.ConvertMMToInch();
					ey = ey.ConvertMMToInch();
					cx = cx.ConvertMMToInch();
					cy = cy.ConvertMMToInch();
					lineWidth = lineWidth.ConvertUMToInch();
				}
			}

			double radius = Math.Sqrt(Math.Pow(sx - cx, 2) + Math.Pow(-(sy - cy), 2));
			double startAngle = Math.Atan2(-(sy - cy), sx - cx) * (180 / Math.PI);
			double endAngle = Math.Atan2(-(ey - cy), ex - cx) * (180 / Math.PI);
			double sweepAngle = (endAngle - startAngle);

			if (arc.IsClockWise is true)
			{
				sweepAngle = sweepAngle <= 0 ?
					(float)sweepAngle + 360.0f : (float)sweepAngle;
			}
			else
			{
				sweepAngle = sweepAngle >= 0 ?
					(float)sweepAngle - 360.0f : (float)sweepAngle;
			}

			return new ShapeArc(pixelResolution,				
				(float)(cx - radius),
				(float)-(cy + radius),
				(float)(radius * 2),
				(float)(radius * 2),
				(float)startAngle,
				(float)sweepAngle,
				(float)lineWidth);
		}

		public static IShapeArc CreateOpenGl(float pixelResolution,
			IFeatureArc arc)
		{
			throw new NotImplementedException();
		}
	}
}
