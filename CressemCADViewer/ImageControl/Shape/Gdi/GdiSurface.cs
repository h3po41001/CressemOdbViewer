using System.Collections.Generic;
using System.Drawing;
using ImageControl.Extension;
using ImageControl.Model.Shape.Gdi;
using ImageControl.Shape.Gdi.Interface;

namespace ImageControl.Shape.Gdi
{
	internal class GdiSurface : GdiShape
	{
		private GdiSurface() { }

		public GdiSurface(bool isPositive, 
			IEnumerable<GdiShapePolygon> polygons) : base()
		{
			IsPositive = isPositive;
			Polygons = new List<GdiShapePolygon>(polygons);
			GraphicsRegion = new Region();
			GraphicsRegion.MakeEmpty();

			foreach (GdiShapePolygon polygon in Polygons)
			{
				if (polygon.IsFill)
				{
					GraphicsRegion.Union(polygon.GraphicsPath);
				}
				else
				{
					GraphicsRegion.Xor(polygon.GraphicsPath);
				}
			}
		}

		public bool IsPositive { get; private set; }

		public IEnumerable<GdiShapePolygon> Polygons { get; private set; }

		public Region GraphicsRegion { get; private set; }

		public override void Fill(Graphics graphics)
		{
			graphics.FillRegion(SolidBrush, GraphicsRegion);
		}

		public override void Draw(Graphics graphics)
		{
			return;
		}
	}
}
