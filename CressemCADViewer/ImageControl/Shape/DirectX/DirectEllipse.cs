using System.Drawing;
using ImageControl.Shape.DirectX.Interface;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;

namespace ImageControl.Shape.DirectX
{
	internal class DirectEllipse : DirectShape
	{

		private DirectEllipse() { }

		public DirectEllipse(float sx, float sy, float width, float height,
			Factory factory, RenderTarget render, Color color) : base(factory, render, color)
		{
			Sx = sx;
			Sy = sy;
			Width = width;
			Height = height;

			SetShape();
		}

		public float Sx { get; private set; }

		public float Sy { get; private set; }

		public float Width { get; private set; }

		public float Height { get; private set; }


		public Ellipse Ellipse { get; private set; }

		public override void SetShape()
		{
			float radiusWidth = Width / 2;
			float radiusHeight = Height / 2;

			Ellipse = new Ellipse(new RawVector2(
				Sx + radiusWidth, Sy + radiusHeight), 
				radiusWidth, radiusHeight);
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
