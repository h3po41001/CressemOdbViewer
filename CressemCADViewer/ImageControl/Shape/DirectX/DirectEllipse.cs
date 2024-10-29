using System.Drawing;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;

namespace ImageControl.Shape.DirectX
{
	internal class DirectEllipse : DirectShape
	{
		private DirectEllipse() { }

		public DirectEllipse(bool isPositive,
			float sx, float sy, float width, float height,
			Factory factory, RenderTarget render, Color color) : base(isPositive, factory, render, color)
		{
			Width = width / 2;
			Height = height / 2;
			StartPt = new RawVector2(sx + width, sy + height);

			SetShape();
		}

		public RawVector2 StartPt { get; private set; }

		public float Width { get; private set; }

		public float Height { get; private set; }

		public Ellipse Ellipse { get; private set; }

		public override void SetShape()
		{
			Ellipse = new Ellipse(StartPt, Width, Height);
			ShapeGemotry = new EllipseGeometry(Factory, Ellipse);
		}

		public override RectangleF GetBounds()
		{
			return new RectangleF(StartPt.X - Width, StartPt.Y - Height, Width * 2, Height * 2);
		}

		public override void Draw(RenderTarget render)
		{
			render.DrawEllipse(Ellipse, ProfileBrush);
		}

		public override void Fill(RenderTarget render)
		{
			if (IsPositive)
			{
				render.FillEllipse(Ellipse, DefaultBrush);
			}
			else
			{
				render.FillEllipse(Ellipse, HoleBrush);
			}
		}
	}
}
