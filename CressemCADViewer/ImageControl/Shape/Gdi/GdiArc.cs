using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using ImageControl.Shape.Interface;

namespace ImageControl.Model.Shape.Gdi
{
	internal class GdiArc : GdiShape, IShapeArc
	{
		private GdiArc() { }

		public GdiArc(IShapeArc shape) : base(shape.PixelResolution)
		{
			X = shape.X;
			Y = shape.Y;
			Width = shape.Width;
			Height = shape.Height;
			StartAngle = shape.StartAngle;
			SweepAngle = shape.SweepAngle;

		}

		public float X { get; private set; }

		public float Y { get; private set; }

		public float Width { get; private set; }

		public float Height { get; private set; }

		public float StartAngle { get; private set; }

		public float SweepAngle { get; private set; }

		public override void Draw(Graphics graphics)
		{
			graphics.DrawArc(
				DefaultPen,
				X * PixelResolution,
				Y * PixelResolution,
				Width * PixelResolution, 
				Height * PixelResolution,
				StartAngle, 
				SweepAngle);
		}
	}
}
