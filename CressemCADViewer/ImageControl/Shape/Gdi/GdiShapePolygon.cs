using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using ImageControl.Extension;
using ImageControl.Shape.Gdi.Interface;

namespace ImageControl.Model.Shape.Gdi
{
	internal class GdiShapePolygon : GdiShape, IGdiPolygon
	{
		private SolidBrush _holeBrush = null;

		private GdiShapePolygon() { }

		public GdiShapePolygon(bool isFill,
			IEnumerable<IGdiBase> shapes) : base()
		{
			IsFill = isFill;

			Shapes = new List<IGdiBase>(shapes);
			GraphicsPath = new GraphicsPath();
			_holeBrush = new SolidBrush(Color.Black);
			ProfilePen.Color = Color.White;

			foreach (var shape in Shapes)
			{
				GraphicsPath.Add(shape);
			}
		}

		public bool IsFill { get; private set; }

		public IEnumerable<PointF> Points { get; private set; }

		public IEnumerable<IGdiBase> Shapes { get; private set; }

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
