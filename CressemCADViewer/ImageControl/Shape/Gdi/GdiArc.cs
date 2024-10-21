using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using ImageControl.Shape.Interface;

namespace ImageControl.Model.Shape.Gdi
{
	internal class GdiArc : GdiShape
	{
		private GdiArc() { }

		public GdiArc(float pixelResolution,
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
