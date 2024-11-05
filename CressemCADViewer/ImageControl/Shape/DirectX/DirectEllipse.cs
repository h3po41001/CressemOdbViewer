using System;
using System.Drawing;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;

namespace ImageControl.Shape.DirectX
{
	internal class DirectEllipse : DirectShape
	{
		private DirectEllipse() { }

		public DirectEllipse(bool isPositive,
			float cx, float cy, float radiusX, float radiusY,
			Factory factory, RenderTarget render, Color color,
			float skipRatio) : base(isPositive, factory, render, color)
		{
			RadiusX = radiusX;
			RadiusY = radiusY;
			CenterPt = new RawVector2(cx, cy);

			SetShape(skipRatio);
		}

		public RawVector2 CenterPt { get; private set; }

		public float RadiusX { get; private set; }

		public float RadiusY { get; private set; }

		public Ellipse Ellipse { get; private set; }

		public override void SetShape(float skipRatio)
		{
			Ellipse = new Ellipse(CenterPt, RadiusX, RadiusY);
			ShapeGemotry = new EllipseGeometry(Factory, Ellipse);
			Bounds = new RectangleF(CenterPt.X - RadiusX,
				CenterPt.Y - RadiusY, RadiusX * 2, RadiusY * 2);
			SkipSize = new SizeF(
				Math.Abs(Bounds.Width * skipRatio),
				Math.Abs(Bounds.Height * skipRatio));
		}

		public override void Draw(RenderTarget render)
		{
			render.DrawEllipse(Ellipse, ProfileBrush);
		}

		public override void Fill(RenderTarget render,
			bool isHole, RectangleF roi)
		{
			// 확대한 shape 크기가 roi 보다 커야됨. (작지 않아서 그려도 되는것)
			if (IsPositive != isHole)
			{
				if (SkipSize.Width >= roi.Width &&
					SkipSize.Height >= roi.Height)
				{
					if (roi.IntersectsWith(Bounds) is true)
					{
						render.FillEllipse(Ellipse, DefaultBrush);
					}
				}
			}
			else
			{
				render.FillEllipse(Ellipse, HoleBrush);
			}
		}
	}
}
