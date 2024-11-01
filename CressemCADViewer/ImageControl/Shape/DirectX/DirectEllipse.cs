using System.Drawing;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;

namespace ImageControl.Shape.DirectX
{
	internal class DirectEllipse : DirectShape
	{
		private DirectEllipse() { }

		public DirectEllipse(bool isPositive,
			float cx, float cy, float radiusX, float radiusY,
			Factory factory, RenderTarget render, Color color) : base(isPositive, factory, render, color)
		{
			RadiusX = radiusX;
			RadiusY = radiusY;
			CenterPt = new RawVector2(cx, cy);

			SetShape();
		}

		public RawVector2 CenterPt { get; private set; }

		public float RadiusX { get; private set; }

		public float RadiusY { get; private set; }

		public Ellipse Ellipse { get; private set; }

		public override void SetShape()
		{
			Ellipse = new Ellipse(CenterPt, RadiusX, RadiusY);
			ShapeGemotry = new EllipseGeometry(Factory, Ellipse);
			Bounds = new RectangleF(CenterPt.X - RadiusX, 
				CenterPt.Y - RadiusY, RadiusX * 2, RadiusY * 2);
		}

		public override void Draw(RenderTarget render, RectangleF roi)
		{
			if (roi.IntersectsWith(Bounds) is true)
			{
				render.DrawEllipse(Ellipse, ProfileBrush);
			}
		}

		public override void Fill(RenderTarget render, bool isHole, RectangleF roi)
		{
			//if (roi.IntersectsWith(Bounds) is true)
			{
				if (IsPositive != isHole)
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
}
