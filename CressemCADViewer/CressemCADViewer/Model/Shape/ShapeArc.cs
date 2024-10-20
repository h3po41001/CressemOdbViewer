using System;
using CressemExtractLibrary.Data.Interface.Features;
using ImageControl.Shape.Interface;

namespace CressemCADViewer.Model.Shape
{
	internal class ShapeArc : ShapeBase, IShapeArc
	{
		private ShapeArc() { }

		public ShapeArc(float pixelResolution,
			float x, float y,
			float width, float height,
			float startAngle, float sweepAngle) : base(pixelResolution)
		{
			X = x;
			Y = y;
			Width = width;
			Height = height;
			StartAngle = startAngle;
			SweepAngle = sweepAngle;
		}

		public float X { get; private set; }

		public float Y { get; private set; }

		public float Width { get; private set; }

		public float Height { get; private set; }

		public float StartAngle { get; private set; }

		public float SweepAngle { get; private set; }

		public static IShapeArc Create(float pixelResolution,
			IFeatureArc arc)
		{
			double radius = Math.Sqrt(Math.Pow(arc.X - arc.Cx, 2) + Math.Pow(-(arc.Y - arc.Cy), 2));
			double startAngle = Math.Atan2(-(arc.Y - arc.Cy), arc.X - arc.Cx) * (180 / Math.PI);
			double endAngle = Math.Atan2(-(arc.Ey - arc.Cy), arc.Ex - arc.Cx) * (180 / Math.PI);
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
				(float)(arc.Cx - radius),
				(float)(-arc.Cy - radius),
				(float)(radius * 2),
				(float)(radius * 2),
				(float)startAngle,
				(float)sweepAngle);
		}
	}
}
