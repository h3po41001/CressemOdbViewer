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
			Factory factory, RenderTarget render, Color color) : base(isPositive, factory, render, color)
		{
			StartPt = new RawVector2(sx, sy);
			EndPt = new RawVector2(ex, ey);
			Size = new Size2F(width, height);
			Rotation = rotaion;
			ArcSz = isLargeArc ? ArcSize.Large : ArcSize.Small;
			SweepDir = isClockwise ? SweepDirection.Clockwise : SweepDirection.CounterClockwise;
			LineWidth = lineWidth;

			SetShape();
		}

		public RawVector2 StartPt { get; private set; }

		public RawVector2 EndPt { get; private set; }

		public Size2F Size { get; private set; }

		public float Rotation { get; private set; }

		public SweepDirection SweepDir { get; private set; }

		public ArcSize ArcSz { get; private set; }

		public float LineWidth { get; private set; }

		public ArcSegment Arc { get; private set; }

		public override void SetShape()
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
		}

		public override void Draw(RenderTarget render)
		{
			render.DrawGeometry(ShapeGemotry, ProfileBrush, LineWidth);
		}

		public override void Fill(RenderTarget render, bool isHole,
			RectangleF roi, float skipRatio)
		{
			if (roi.IntersectsWith(Bounds) is true)
			{
				if (Bounds.Width >= roi.Width * skipRatio &&
					Bounds.Height >= roi.Height * skipRatio)
				{
					if (IsPositive != isHole)
					{
						render.DrawGeometry(ShapeGemotry, DefaultBrush, LineWidth);
					}
					else
					{
						render.DrawGeometry(ShapeGemotry, HoleBrush, LineWidth);
					}
				}
			}
		}
	}
}
