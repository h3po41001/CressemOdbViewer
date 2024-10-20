using System.Drawing;
using System.Drawing.Drawing2D;

namespace ImageControl.Model.Gdi.Shape
{
	internal class GdiPie : GdiShape
	{
		private GdiPie() { }

		public GdiPie(float x, float y, float width, float height, 
			float startAngle, float sweepAngle,
			float pixelResolution) : base(pixelResolution)
		{
			X = x;
			Y = y;
			Width = width;
			Height = height;
			StartAngle = startAngle;
			SweepAngle = sweepAngle;
		}

		public GdiPie(RectangleF rectangle, float startAngle, float sweepAngle,
			float pixelResolution) : base(pixelResolution)
		{
			X = rectangle.X;
			Y = rectangle.Y;
			Width = rectangle.Width;
			Height = rectangle.Height;
			StartAngle = startAngle;
			SweepAngle = sweepAngle;
		}

		public float X { get; private set; }

		public float Y { get; private set; }

		public float Width { get; private set; }

		public float Height { get; private set; }

		public float StartAngle { get; private set; }

		public float SweepAngle { get; private set; }

		public override void Draw(Graphics graphics)
		{
			graphics.DrawPie(DefaultPen, X, Y, Width, Height, StartAngle, SweepAngle);
		}

		public override void AddPath(GraphicsPath path)
		{
			throw new System.NotImplementedException();
		}
	}
}
