using System.Drawing;
using ImageControl.Shape.DirectX.Interface;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;

namespace ImageControl.Shape.DirectX
{
	internal class DirectEllipse : DirectShape, IDirectEllipse
	{

		private DirectEllipse() { }

		public DirectEllipse(float cx, float cy, float width, float height,
			Factory factory, RenderTarget render, Color color) : base(factory, render, color)
		{
			Cx = cx;
			Cy = cy;
			Width = width;
			Height = height;

			SetShape();
		}

		public float Cx { get; private set; }

		public float Cy { get; private set; }

		public float Width { get; private set; }

		public float Height { get; private set; }


		public Ellipse Ellipse { get; private set; }

		public override void SetShape()
		{
			Ellipse = new Ellipse(new RawVector2(Cx, Cy), Width / 2, Height / 2);
		}

		public override void Draw(RenderTarget render)
		{
			render.DrawEllipse(Ellipse, Brush);
		}

		public override void Fill(RenderTarget render)
		{
			render.FillEllipse(Ellipse, Brush);
		}
	}
}
