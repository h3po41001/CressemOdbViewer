using System.Drawing;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;

namespace ImageControl.Shape.DirectX
{
	internal class DirectLine : DirectShape
	{
		private DirectLine() : base() { }

		public DirectLine(bool isPositive,
			float sx, float sy, float ex, float ey,
			float lineWidth,
			Factory factory, RenderTarget render, Color color) : base(isPositive, factory, render, color)
		{
			StartPt = new RawVector2(sx, sy);
			EndPt = new RawVector2(ex, ey);
			LineWidth = lineWidth > 0 ? lineWidth : 0.01f;

			SetShape();
		}

		public RawVector2 StartPt { get; private set; }

		public RawVector2 EndPt { get; private set; }

		public float LineWidth { get; private set; }

		public override void SetShape()
		{
			ShapeGemotry = new PathGeometry(Factory);
			using (GeometrySink sink = ((PathGeometry)ShapeGemotry).Open())
			{
				sink.BeginFigure(StartPt, FigureBegin.Filled);
				sink.AddLine(EndPt);
				sink.EndFigure(FigureEnd.Open);
				sink.Close();
			}
		}

		public override RectangleF GetBounds()
		{
			return new RectangleF(StartPt.X, StartPt.Y, EndPt.X - StartPt.X, EndPt.Y - StartPt.Y);
		}

		public override void Draw(RenderTarget render)
		{
			render.DrawLine(StartPt, EndPt, ProfileBrush, LineWidth);
		}

		public override void Fill(RenderTarget render, bool isHole)
		{
			if (IsPositive != isHole)
			{
				//render.DrawLine(StartPt, EndPt, DefaultBrush, LineWidth);
				render.FillGeometry(ShapeGemotry, DefaultBrush);
			}
			else
			{
				//render.DrawLine(StartPt, EndPt, HoleBrush, LineWidth);
				render.FillGeometry(ShapeGemotry, HoleBrush);
			}
		}
	}
}
