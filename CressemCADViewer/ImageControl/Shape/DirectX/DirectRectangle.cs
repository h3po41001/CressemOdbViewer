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
			Factory factory, RenderTarget render, Color color) : base(isPositive, factory, render, color)
		{
			Left = left;
			Top = top;
			Right = right;
			Bottom = bottom;

			SetShape();
		}

		public float Left { get; private set; }

		public float Top { get; private set; }

		public float Right { get; private set; }

		public float Bottom { get; private set; }

		public RawRectangleF Rectangle { get; private set; }

		public override void SetShape()
		{
			Rectangle = new RawRectangleF(Left, Top, Right, Bottom);
			ShapeGemotry = new RectangleGeometry(Factory, Rectangle);
			Bounds = new RectangleF(Left, Top, Right - Left, Bottom - Top);
		}

		public override void Draw(RenderTarget render, RectangleF roi)
		{
			if (roi.IntersectsWith(Bounds) is true)
			{
				render.DrawRectangle(Rectangle, ProfileBrush);
			}
		}

		public override void Fill(RenderTarget render, bool isHole, RectangleF roi)
		{
			//if (roi.IntersectsWith(Bounds) is true)
			{
				if (IsPositive != isHole)
				{
					render.FillRectangle(Rectangle, DefaultBrush);
				}
				else
				{
					render.FillRectangle(Rectangle, HoleBrush);
				}
			}
		}
	}
}
