using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using ImageControl.Shape;
using ImageControl.Shape.Interface;

namespace ImageControl.Model.Shape.Gdi
{
	internal class GdiGraphicsPath : GdiShape
	{
		private GdiGraphicsPath() { }

		public GdiGraphicsPath(IShapePolygon shapeSurface) :
			base(shapeSurface.PixelResolution)
		{
			IsFill = shapeSurface.IsFill;

			Shapes = new List<GdiShape>();
			GraphicsPath = new GraphicsPath();

			foreach (var shape in shapeSurface.Shapes)
			{
				AddShape(ShapeFactory.Instance.CreateGdiShape(shape));
			}
		}

		public bool IsFill { get; private set; }

		public List<GdiShape> Shapes { get; private set; }

		public GraphicsPath GraphicsPath { get; private set; }

		public override void Draw(Graphics graphics)
		{
			if (IsFill)
				graphics.FillPath(new SolidBrush(Color.White), GraphicsPath);
			else
				graphics.FillPath(new SolidBrush(Color.Black), GraphicsPath);
		}

		private void AddShape(GdiShape shape)
		{
			if (shape is GdiArc arc)
			{
				GraphicsPath.AddArc(
					arc.X * PixelResolution, arc.Y * PixelResolution,
					arc.Width * PixelResolution, arc.Height * PixelResolution,
					arc.StartAngle, arc.SweepAngle);
			}
			else if (shape is GdiEllipse ellipse)
			{
				GraphicsPath.AddEllipse(
					ellipse.X * PixelResolution,
					ellipse.Y * PixelResolution,
					ellipse.Width * PixelResolution,
					ellipse.Height * PixelResolution);
			}
			else if (shape is GdiLine line)
			{
				GraphicsPath.AddLine(
					line.Sx * PixelResolution, line.Sy * PixelResolution,
					line.Ex * PixelResolution, line.Ey * PixelResolution);
			}
			else if (shape is GdiRectangle rectangle)
			{
				GraphicsPath.AddRectangle(new RectangleF(
					rectangle.X * PixelResolution,
					rectangle.Y * PixelResolution,
					rectangle.Width * PixelResolution,
					rectangle.Height * PixelResolution));
			}

			Shapes.Add(shape);
		}
	}
}
