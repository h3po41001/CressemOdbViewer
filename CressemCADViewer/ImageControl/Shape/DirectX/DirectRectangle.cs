using System;
using System.Drawing;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;

namespace ImageControl.Shape.DirectX
{
	internal class DirectRectangle : DirectShape
	{
		private DirectRectangle() : base() { }

		public DirectRectangle(bool isPositive,
			float left, float top, float right, float bottom,
			Factory factory, RenderTarget render, Color color,
			float skipRatio) : base(isPositive, factory, render, color)
		{
			Left = left;
			Top = top;
			Right = right;
			Bottom = bottom;

			SetShape(skipRatio);
		}

		public float Left { get; private set; }

		public float Top { get; private set; }

		public float Right { get; private set; }

		public float Bottom { get; private set; }

		public RawRectangleF Rectangle { get; private set; }

		public override void SetShape(float skipRatio)
		{
			Rectangle = new RawRectangleF(Left, Top, Right, Bottom);
			ShapeGemotry = new RectangleGeometry(Factory, Rectangle);

			Bounds = new RectangleF(Left, Top, Right - Left, Bottom - Top);
			SkipSize = new SizeF(
				Math.Abs(Bounds.Width * skipRatio),
				Math.Abs(Bounds.Height * skipRatio));
		}

		public override void Draw(RenderTarget render)
		{
			render.DrawRectangle(Rectangle, ProfileBrush);
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
						render.FillRectangle(Rectangle, DefaultBrush);
					}
				}
			}
			else
			{
				render.FillRectangle(Rectangle, HoleBrush);
			}
		}
	}
}
