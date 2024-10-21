using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ImageControl.Model.Shape.Gdi;

namespace ImageControl.Shape.Gdi
{
	internal class GdiSurface : GdiShape
	{
		private GdiSurface() { }

		public GdiSurface(float pixelResolution,
			bool isPositive, IEnumerable<GdiPolygon> polygons) : base(pixelResolution) 
		{
			IsPositive = isPositive;
			Polygons = new List<GdiPolygon>(polygons);
		}

		public bool IsPositive { get; private set; }

		public List<GdiPolygon> Polygons { get; private set; }

		public override void Draw(Graphics graphics)
		{
			foreach (var polygon in Polygons)
			{
				polygon.Draw(graphics);
			}
		}
	}
}
