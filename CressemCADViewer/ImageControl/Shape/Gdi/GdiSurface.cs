using System.Collections.Generic;
using System.Drawing;
using ImageControl.Model.Shape.Gdi;

namespace ImageControl.Shape.Gdi
{
	internal class GdiSurface : GdiShape
	{
		private GdiSurface() { }

		public GdiSurface(float pixelResolution,
			bool isPositive, IEnumerable<GdiShapePolygon> polygons) : base(pixelResolution) 
		{
			IsPositive = isPositive;
			Polygons = new List<GdiShapePolygon>(polygons);
		}

		public bool IsPositive { get; private set; }

		public List<GdiShapePolygon> Polygons { get; private set; }

		public override void Draw(Graphics graphics)
		{
			foreach (var polygon in Polygons)
			{
				polygon.Draw(graphics);
			}
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
