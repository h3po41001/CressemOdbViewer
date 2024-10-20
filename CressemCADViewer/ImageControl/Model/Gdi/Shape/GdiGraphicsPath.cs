using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ImageControl.Model.Gdi.Shape
{
	public class GdiGraphicsPath : GdiShape
	{
		private GdiGraphicsPath() { }

		public GdiGraphicsPath(bool isFill, float pixelResolution) : base(pixelResolution)
		{
			IsFill = isFill;
			Shapes = new List<GdiShape>();
			GraphicsPath = new GraphicsPath();
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

		public override void AddPath(GraphicsPath path)
		{
			path.AddPath(GraphicsPath, false);
		}

		public void AddShape(GdiShape shape)
		{
			shape.AddPath(GraphicsPath);
			Shapes.Add(shape);
		}
	}
}
