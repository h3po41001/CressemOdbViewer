using System.Drawing;
using ImageControl.Shape.DirectX.Interface;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;

namespace ImageControl.Shape.DirectX
{
	internal class DirectRectangle : DirectShape, IDirectRectangle
	{
		private DirectRectangle() : base() { }

		public DirectRectangle(float left, float top, float right, float bottom,
			Factory factory, RenderTarget render, Color color) : base(factory, render, color)
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
		}

		public override void Draw(RenderTarget render)
		{
			throw new System.NotImplementedException();
		}

		public override void Fill(RenderTarget render)
		{
			throw new System.NotImplementedException();
		}
	}
}
