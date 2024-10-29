using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using ImageControl.Extension;

namespace ImageControl.Model.Shape.Gdi
{
	internal class GdiShapePolygon : GdiShape
	{
		private GdiShapePolygon() : base() { }

		public GdiShapePolygon(bool isPositive, bool isFill,
			IEnumerable<GdiShape> shapes) : base(isPositive)
		{
			IsFill = isFill;

			Shapes = new List<GdiShape>(shapes);
			GraphicsPath = new GraphicsPath();
			ProfilePen.Color = Color.White;

			foreach (var shape in Shapes)
			{
				GraphicsPath.Add(shape);
			}

			HoleBrush = new SolidBrush(Color.Black);
		}

		public bool IsFill { get; private set; }

		public IEnumerable<GdiShape> Shapes { get; private set; }

		public GraphicsPath GraphicsPath { get; private set; }

		public SolidBrush HoleBrush { get; private set; }

		public override void Fill(Graphics graphics)
		{
			if (IsFill)
			{
				graphics.FillPath(SolidBrush, GraphicsPath);
			}
			else
			{
				graphics.FillPath(HoleBrush, GraphicsPath);
			}
		}

		public override void Draw(Graphics graphics)
		{
			graphics.DrawPath(ProfilePen, GraphicsPath);
		}
	}
}
