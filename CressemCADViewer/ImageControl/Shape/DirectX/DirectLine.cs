using System.Drawing;
using ImageControl.Shape.DirectX.Interface;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;

namespace ImageControl.Shape.DirectX
{
	internal class DirectLine : DirectPathGeometry, IDirectLine
	{
		private DirectLine() : base() { }

		public DirectLine(float sx, float sy, float ex, float ey,
			Factory factory, RenderTarget render, Color color) : base(factory, render, color)
		{
			Sx = sx;
			Sy = sy;
			Ex = ex;
			Ey = ey;

			SetShape();
		}

		public float Sx { get; private set; }

		public float Sy { get; private set; }

		public float Ex { get; private set; }

		public float Ey { get; private set; }

		public override void SetShape()
		{
			PathGeometry = new PathGeometry(Factory);
			try
			{
				RawVector2 startPoint = new RawVector2(Sx, Sy);

				using (GeometrySink sink = PathGeometry.Open())
				{
					sink.BeginFigure(startPoint, FigureBegin.Filled);
					sink.AddLine(new RawVector2(Ex, Ey));
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
