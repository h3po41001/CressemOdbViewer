using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace ImageControl.Model.Gdi.Shape
{
	internal class GdiGraphicsPath
	{
		private GdiGraphicsPath() { }

		public GdiGraphicsPath(IEnumerable<GdiShape> shapes)
		{
			Shapes = shapes;
			GraphicsPath = new GraphicsPath();
		}

		public IEnumerable<GdiShape> Shapes { get; private set; }

		public GraphicsPath GraphicsPath { get; private set; }

		public void Draw(Graphics graphics, Pen pen)
		{
			foreach (var shape in Shapes)
			{
				shape.AddPath(GraphicsPath, pen);
			}

			graphics.FillPath(new SolidBrush(pen.Color), GraphicsPath);
		}
	}
}
