using System.Drawing;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;

namespace ImageControl.Shape.DirectX
{
	internal class DirectArc : DirectShape
	{
		private DirectArc() { }

		public DirectArc(float sx, float sy, float ex, float ey,
			float width, float height, float rotaion, bool isLargeArc, bool isClockwise,
			Factory factory, RenderTarget render, Color color) : base(factory, render, color)
		{
			SetArc(sx, sy, ex, ey, width, height,
				rotaion, isLargeArc, isClockwise);
		}

		public PathGeometry PathGeometry { get; private set; }

		public override void Draw()
		{
			Render.DrawGeometry(PathGeometry, Brush);
		}

		public override void Fill(RenderTarget render)
		{
			render.FillGeometry(PathGeometry, Brush);
		}

		private void SetArc(float sx, float sy, float ex, float ey,
			float width, float height, float rotaion, bool isLargeArc, bool isClockwise)
		{
			// 경로 그리기
			PathGeometry = new PathGeometry(Factory);
			try
			{
				RawVector2 startPoint = new RawVector2(sx, sy);
				ArcSegment arc = new ArcSegment()
				{
					Point = new RawVector2(ex, ey),
					Size = new Size2F(width / 2, height / 2),
					RotationAngle = rotaion,
					SweepDirection = isClockwise ? SweepDirection.Clockwise : SweepDirection.CounterClockwise,
					ArcSize = isLargeArc ? ArcSize.Large : ArcSize.Small
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
	}
}
