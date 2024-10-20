using System.Drawing;
using System.Drawing.Drawing2D;
using ImageControl.Shape.Interface;

namespace ImageControl.Model.Shape.Gdi
{
	internal class GdiEllipse : GdiShape
	{
		private GdiEllipse() { }

		public GdiEllipse(IShapeEllipse shapeEllipse) : base(shapeEllipse.PixelResolution)
		{
			X = shapeEllipse.X;
			Y = -shapeEllipse.Y;
			Width = shapeEllipse.Width;
			Height = shapeEllipse.Height;
		}

		public float X { get; private set; }

		public float Y { get; private set; }

		public float Width { get; private set; }

		public float Height { get; private set; }

		public override void Draw(Graphics graphics)
		{
			graphics.DrawEllipse(DefaultPen,
				X * PixelResolution,
				Y * PixelResolution,
				Width * PixelResolution,
				Height * PixelResolution);
		}
	}
}
