using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using ImageControl.Extension;

namespace ImageControl.Model.Shape.Gdi
{
	internal class GdiPolygon : GdiShape
	{
		private GdiPolygon() { }

		public GdiPolygon(float pixelResolution, bool isFill,
			IEnumerable<GdiShape> shapes) : base(pixelResolution)
		{
			IsFill = isFill;

			Shapes = new List<GdiShape>(shapes);
			GraphicsPath = new GraphicsPath();

			foreach (var shape in Shapes)
			{
				GraphicsPath.Add(shape, pixelResolution);
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
	}
}
