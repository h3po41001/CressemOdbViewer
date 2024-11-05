using System.Collections.Generic;
using CressemExtractLibrary.Data.Interface.Features;
using ImageControl.Shape.Gdi.Interface;
using ImageControl.Shape.Interface;

namespace CressemDataToGraphics.Model.Graphics.Shape
{
	internal class ShapeGdiSurface : ShapeGraphicsBase, IGdiSurface
	{
		private ShapeGdiSurface() : base()
		{
		}

		public ShapeGdiSurface(bool isPositive,
			IEnumerable<IGraphicsShape> polygons) : base()
		{
			IsPositive = isPositive;
			Polygons = polygons;
		}

		public bool IsPositive { get; private set; }

		public IEnumerable<IGraphicsShape> Polygons { get; private set; }

		public static ShapeGdiSurface Create(bool useMM, float pixelResolution,
			int globalOrient, bool isGlobalFlipHorizontal,
			bool isMM, double datumX, double datumY,
			double cx, double cy,
			int orient, bool isFlipHorizontal,
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
					pixelResolution, globalOrient, isGlobalFlipHorizontal,
					isMM, datumX, datumY, cx, cy,
					orient, isFlipHorizontal, isPositive, polygon));
			}

			return new ShapeGdiSurface(isPositive, polygons);
		}
	}
}
