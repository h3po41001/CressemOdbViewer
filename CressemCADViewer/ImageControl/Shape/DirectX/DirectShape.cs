using System.Drawing;
using ImageControl.Shape.DirectX.Interface;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;

namespace ImageControl.Shape.DirectX
{
	internal abstract class DirectShape
	{
		protected DirectShape() { }

		protected DirectShape(Factory factory, RenderTarget render, Color color)
		{
			Factory = factory;
			Render = render;
			Brush = new SolidColorBrush(Render,
				new RawColor4(color.R / 255.0f, color.G / 255.0f, color.B / 255.0f, color.A / 255.0f));
		}

		public SolidColorBrush Brush { get; private set; }

		protected Factory Factory { get; private set; }

		protected RenderTarget Render { get; private set; }

		public abstract void SetShape();

		public abstract void Draw(RenderTarget render);

		public abstract void Fill(RenderTarget render);
	}
}
