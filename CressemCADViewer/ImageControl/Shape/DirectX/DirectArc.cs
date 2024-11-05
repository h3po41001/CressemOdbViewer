using System;
using System.Drawing;
using ImageControl.Extension;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;

namespace ImageControl.Shape.DirectX
{
	internal class DirectArc : DirectShape
	{
		private DirectArc() : base() { }

		public DirectArc(bool isPositive,
			float sx, float sy, float ex, float ey,
			float width, float height, float rotaion,
			bool isLargeArc, bool isClockwise,
			float lineWidth,
			Factory factory, RenderTarget render, Color color,
			float skipRatio) : base(isPositive, factory, render, color)
		{
			StartPt = new RawVector2(sx, sy);
			EndPt = new RawVector2(ex, ey);
			Size = new Size2F(width, height);
			Rotation = rotaion;
			ArcSz = isLargeArc ? ArcSize.Large : ArcSize.Small;
			SweepDir = isClockwise ? SweepDirection.Clockwise : SweepDirection.CounterClockwise;
			LineWidth = lineWidth;

			SetShape(skipRatio);
		}

		public RawVector2 StartPt { get; private set; }

		public RawVector2 EndPt { get; private set; }

		public Size2F Size { get; private set; }

		public float Rotation { get; private set; }

		public SweepDirection SweepDir { get; private set; }

		public ArcSize ArcSz { get; private set; }

		public float LineWidth { get; private set; }

		public ArcSegment Arc { get; private set; }

		public override void SetShape(float skipRatio)
		{
			Arc = new ArcSegment()
			{
				Point = EndPt,
				Size = Size,
				RotationAngle = Rotation,
				SweepDirection = SweepDir,
				ArcSize = ArcSz
			};

			ShapeGemotry = new PathGeometry(Factory);
			using (GeometrySink sink = ((PathGeometry)ShapeGemotry).Open())
			{
				sink.BeginFigure(StartPt, FigureBegin.Filled);
				sink.AddArc(Arc);
				sink.EndFigure(FigureEnd.Open);
				sink.Close();
			}

			Bounds = ShapeGemotry.GetBounds().ToRectangleF();
			SkipSize = new SizeF(
				Math.Abs(Bounds.Width * skipRatio),
				Math.Abs(Bounds.Height * skipRatio));
		}

		public override void Draw(RenderTarget render)
		{
			render.DrawGeometry(ShapeGemotry, ProfileBrush, LineWidth);
		}

		public override void Fill(RenderTarget render,
			bool isHole, RectangleF roi)
		{
			// 확대한 shape 크기가 roi 보다 커야됨. (작지 않아서 그려도 되는것)
			if (IsPositive != isHole)
			{
				if (SkipSize.Width >= roi.Width &&
					SkipSize.Height >= roi.Height)
				{
					if (roi.IntersectsWith(Bounds) is true)
					{
						render.DrawGeometry(ShapeGemotry, DefaultBrush, LineWidth);
					}
				}
			}
			else
			{
				render.DrawGeometry(ShapeGemotry, HoleBrush, LineWidth);
			}
		}
	}
}
