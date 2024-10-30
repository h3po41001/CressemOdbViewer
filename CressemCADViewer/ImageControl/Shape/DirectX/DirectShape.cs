using System.Drawing;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;

namespace ImageControl.Shape.DirectX
{
	internal abstract class DirectShape
	{
		protected DirectShape() { }

		protected DirectShape(bool isPositive, 
			Factory factory, RenderTarget render, Color color)
		{
			IsPositive = isPositive;
			Factory = factory;
			Render = render;

			DefaultBrush = new SolidColorBrush(Render,
				new RawColor4(color.R / 255.0f, color.G / 255.0f, color.B / 255.0f, color.A / 255.0f));

			HoleBrush = new SolidColorBrush(Render,
				new RawColor4(1, 0, 0, 1f));

			ProfileBrush = new SolidColorBrush(Render,
				new RawColor4(1, 1, 1, 1));
		}

		public bool IsPositive { get; private set; }

		public Geometry ShapeGemotry { get; protected set; }

		public SolidColorBrush DefaultBrush { get; private set; }

		public SolidColorBrush HoleBrush { get; private set; }

		public SolidColorBrush ProfileBrush { get; private set; }

		protected Factory Factory { get; private set; }

		protected RenderTarget Render { get; private set; }

		public abstract void SetShape();

		public abstract RectangleF GetBounds();

		public abstract void Draw(RenderTarget render);

		public abstract void Fill(RenderTarget render, bool isHole);
	}
}
