using System.Drawing;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;

namespace ImageControl.Shape.DirectX
{
	internal class DirectArc : DirectPathGeometry
	{
		private DirectArc() : base() { }

		public DirectArc(float sx, float sy, float ex, float ey,
			float width, float height, float rotaion, bool isLargeArc, bool isClockwise,
			Factory factory, RenderTarget render, Color color) : base(factory, render, color)
		{
			Sx = sx;
			Sy = sy;
			Ex = ex;
			Ey = ey;
			Width = width;
			Height = height;
			Rotation = rotaion;
			IsLargeArc = isLargeArc;
			IsClockwise = isClockwise;

			SetShape();
		}

		public float Sx { get; private set; }

		public float Sy { get; private set; }

		public float Ex { get; private set; }

		public float Ey { get; private set; }

		public float Width { get; private set; }

		public float Height { get; private set; }

		public float Rotation { get; private set; }

		public bool IsLargeArc { get; private set; }

		public bool IsClockwise { get; private set; }

		public override void SetShape()
		{
			// 경로 그리기
			PathGeometry = new PathGeometry(Factory);
			try
			{
				RawVector2 startPoint = new RawVector2(Sx, Sy);
				ArcSegment arc = new ArcSegment()
				{
					Point = new RawVector2(Ex, Ey),
					Size = new Size2F(100f, 100f),//Width / 2, Height / 2),
					RotationAngle = Rotation,
					SweepDirection = IsClockwise ? SweepDirection.Clockwise : SweepDirection.CounterClockwise,
					ArcSize = IsLargeArc ? ArcSize.Large : ArcSize.Small
				};

				using (GeometrySink sink = PathGeometry.Open())
				{
					sink.BeginFigure(startPoint, FigureBegin.Filled);
					sink.AddArc(arc);
					sink.EndFigure(FigureEnd.Closed);
					sink.Close();
				}
			}
			catch (System.Exception)
			{
				PathGeometry.Dispose();
				throw;
			}
		}

		public override void Draw(RenderTarget render)
		{
			Render.DrawGeometry(PathGeometry, Brush);
		}

		public override void Fill(RenderTarget render)
		{
			render.FillGeometry(PathGeometry, Brush);
		}
	}
}
