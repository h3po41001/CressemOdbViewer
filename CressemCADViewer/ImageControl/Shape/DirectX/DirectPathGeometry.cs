using System.Drawing;
using ImageControl.Shape.DirectX.Interface;
using SharpDX.Direct2D1;

namespace ImageControl.Shape.DirectX
{
	internal abstract class DirectPathGeometry : DirectShape, IDirectShape
	{
		protected DirectPathGeometry() : base() { }

		public DirectPathGeometry(Factory factory, RenderTarget render, 
			Color color) : base(factory, render, color)
		{
		}

		public PathGeometry PathGeometry { get; protected set; }

		public override abstract void SetShape();

		public override void Draw(RenderTarget render)
		{
			render.DrawGeometry(PathGeometry, Brush);
		}

		public override void Fill(RenderTarget render)
		{
			render.FillGeometry(PathGeometry, Brush);
		}
	}
}
