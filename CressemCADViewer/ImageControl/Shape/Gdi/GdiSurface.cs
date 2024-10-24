using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using ImageControl.Model.Shape.Gdi;

namespace ImageControl.Shape.Gdi
{
	internal class GdiSurface : GdiShape
	{
		private GdiSurface() { }

		public GdiSurface(float pixelResolution, bool isPositive,
			IEnumerable<GdiShapePolygon> polygons) : base(pixelResolution)
		{
			IsPositive = isPositive;
			Polygons = new List<GdiShapePolygon>(polygons);
			GraphicsRegion = new Region();
			GraphicsRegion.MakeEmpty();

			foreach (var polygon in Polygons)
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

		public List<GdiShapePolygon> Polygons { get; private set; }

		public Region GraphicsRegion { get; private set; }

		public override void Draw(Graphics graphics)
		{
			graphics.FillRegion(new SolidBrush(Color.DarkGreen), GraphicsRegion);
		}

		public override void DrawProfile(Graphics graphics)
		{
			foreach (var polygon in Polygons)
			{
				polygon.DrawProfile(graphics);
			}
		}
	}
}
