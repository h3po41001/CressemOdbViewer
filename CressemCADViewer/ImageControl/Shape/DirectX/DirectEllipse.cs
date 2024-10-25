using System;
using System.Drawing;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;

namespace ImageControl.Shape.DirectX
{
	internal class DirectEllipse : DirectShape
	{

		private DirectEllipse() { }

		public DirectEllipse(float cx, float cy, float width, float height,
			Factory factory, RenderTarget render, Color color) : base(factory, render, color)
		{
			SetEllipse(cx, cy, width, height);
		}

		public Ellipse Ellipse { get; private set; }

		public override void Draw()
		{
			Render.DrawEllipse(Ellipse, Brush);
		}

		public override void Fill(RenderTarget render)
		{
			render.FillEllipse(Ellipse, Brush);
		}

		private void SetEllipse(float cx, float cy, float width, float height)
		{
			Ellipse = new Ellipse(new RawVector2(cx, cy), width / 2, height / 2);
		}
	}
}
