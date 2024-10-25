using System.Collections.Generic;
using System.Drawing;
using ImageControl.Extension;
using ImageControl.Model.Shape.Gdi;
using ImageControl.Shape.Gdi.Interface;

namespace ImageControl.Shape.Gdi
{
	internal class GdiSurface : GdiShape, IGdiSurface
	{
		private GdiSurface() { }

		public GdiSurface(bool isPositive, 
			IEnumerable<IGdiPolygon> polygons) : base()
		{
			IsPositive = isPositive;
			Polygons = new List<IGdiPolygon>(polygons);
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

		public IEnumerable<IGdiPolygon> Polygons { get; private set; }

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
