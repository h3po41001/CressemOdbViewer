using System;
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
			Factory factory, RenderTarget render, Color color,
			float skipRatio) : base(isPositive, factory, render, color)
		{
			StartPt = new RawVector2(sx, sy);
			EndPt = new RawVector2(ex, ey);
			LineWidth = lineWidth > 0 ? lineWidth : 0.1f;

			SetShape(skipRatio);
		}

		public RawVector2 StartPt { get; private set; }

		public RawVector2 EndPt { get; private set; }

		public float LineWidth { get; private set; }

		public override void SetShape(float skipRatio)
		{
			ShapeGemotry = new PathGeometry(Factory);
			using (GeometrySink sink = ((PathGeometry)ShapeGemotry).Open())
			{
				sink.BeginFigure(StartPt, FigureBegin.Filled);
				sink.AddLine(EndPt);
				sink.EndFigure(FigureEnd.Open);
				sink.Close();
			}

			Bounds = new RectangleF()
			{
				X = StartPt.X - LineWidth / 2,
				Y = StartPt.Y - LineWidth / 2,
				Width = EndPt.X - StartPt.X + LineWidth,
				Height = EndPt.Y - StartPt.Y + LineWidth,
			};
		
			SkipSize = new SizeF(
				Math.Abs(Bounds.Width * skipRatio),
				Math.Abs(Bounds.Height * skipRatio));
		}

		public override void Draw(RenderTarget render)
		{
			render.DrawLine(StartPt, EndPt, ProfileBrush, LineWidth);
		}

		public override void Fill(RenderTarget render,
			bool isHole, RectangleF roi)
		{
			// 확대한 shape 크기가 roi 보다 커야됨. (작지 않아서 그려도 되는것)
			if (SkipSize.Width >= roi.Width &&
				SkipSize.Height >= roi.Height)
			{
				if (roi.IntersectsWith(Bounds) is true)
				{
					if (IsPositive != isHole)
					{
						render.DrawLine(StartPt, EndPt, DefaultBrush, LineWidth);
					}
					else
					{
						render.DrawLine(StartPt, EndPt, HoleBrush, LineWidth);
					}
				}
			}
		}
	}
}
