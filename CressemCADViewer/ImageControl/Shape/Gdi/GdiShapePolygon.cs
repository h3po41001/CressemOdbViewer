using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using ImageControl.Extension;

namespace ImageControl.Model.Shape.Gdi
{
	internal class GdiShapePolygon : GdiShape
	{
		private SolidBrush _holeBrush = null;

		private GdiShapePolygon() { }

		public GdiShapePolygon(bool isFill,
			IEnumerable<GdiShape> shapes) : base()
		{
			IsFill = isFill;

			Shapes = new List<GdiShape>(shapes);
			GraphicsPath = new GraphicsPath();
			_holeBrush = new SolidBrush(Color.Black);
			ProfilePen.Color = Color.White;

			foreach (var shape in Shapes)
			{
				GraphicsPath.Add(shape);
			}
		}

		public bool IsFill { get; private set; }

		public IEnumerable<GdiShape> Shapes { get; private set; }

		public GraphicsPath GraphicsPath { get; private set; }

		public override void Fill(Graphics graphics)
		{
			if (IsFill)
				graphics.FillPath(SolidBrush, GraphicsPath);
			else
				graphics.FillPath(_holeBrush, GraphicsPath);			
		}

		public override void Draw(Graphics graphics)
		{
			graphics.DrawPath(ProfilePen, GraphicsPath);
		}
	}
}
