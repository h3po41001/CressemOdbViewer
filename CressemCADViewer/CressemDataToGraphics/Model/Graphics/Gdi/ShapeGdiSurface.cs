using System.Collections.Generic;
using CressemExtractLibrary.Data.Interface.Features;
using ImageControl.Shape.Gdi.Interface;

namespace CressemDataToGraphics.Model.Graphics.Shape
{
	internal class ShapeGdiSurface : ShapeGdiBase, IGdiSurface
	{
		private ShapeGdiSurface() : base()
		{
		}

		public ShapeGdiSurface(bool isPositive,
			IEnumerable<ShapeGdiPolygon> polygons) : base()
		{
			IsPositive = isPositive;
			Polygons = polygons;
		}

		public bool IsPositive { get; private set; }

		public IEnumerable<IGdiPolygon> Polygons { get; private set; }

		public static ShapeGdiSurface Create(bool useMM,
			float pixelResolution, bool isMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis, 
			bool isPositive, IEnumerable<IFeaturePolygon> featurePolygons)
		{
			if (featurePolygons is null)
			{
				return null;
			}

			List<ShapeGdiPolygon> polygons = new List<ShapeGdiPolygon>();

			foreach (var polygon in featurePolygons)
			{
				polygons.Add(ShapeGdiPolygon.Create(useMM,
					pixelResolution, isMM,
					xDatum, yDatum, cx, cy,
					orient, isMirrorXAxis, isPositive, polygon));
			}

			return new ShapeGdiSurface(isPositive, polygons);
		}
	}
}
