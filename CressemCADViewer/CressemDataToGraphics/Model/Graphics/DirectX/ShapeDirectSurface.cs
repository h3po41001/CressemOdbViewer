using System.Collections.Generic;
using CressemExtractLibrary.Data.Interface.Features;
using ImageControl.Shape.DirectX.Interface;
using ImageControl.Shape.Interface;

namespace CressemDataToGraphics.Model.Graphics.DirectX
{
	internal class ShapeDirectSurface : ShapeGraphicsBase, IDirectSurface
	{
		private ShapeDirectSurface() : base()
		{
		}

		public ShapeDirectSurface(bool isPositive,
			IEnumerable<IGraphicsShape> polygons) : base()
		{
			IsPositive = isPositive;
			Polygons = polygons;
		}

		public bool IsPositive { get; private set; }

		public IEnumerable<IGraphicsShape> Polygons { get; private set; }

		public static ShapeDirectSurface Create(bool useMM,
			float pixelResolution, bool isMM,
			double globalDatumX, double globalDatumY,
			double localDatumX, double localDatumY,
			double cx, double cy,
			int orient, bool isFlipHorizontal,
			bool isPositive, IEnumerable<IFeaturePolygon> featurePolygons)
		{
			if (featurePolygons is null)
			{
				return null;
			}

			List<ShapeDirectPolygon> polygons = new List<ShapeDirectPolygon>();

			foreach (var polygon in featurePolygons)
			{
				polygons.Add(ShapeDirectPolygon.Create(useMM,
					pixelResolution, isMM,
					globalDatumX, globalDatumY,
					localDatumX, localDatumY, cx, cy,
					orient, isFlipHorizontal, isPositive, polygon));
			}

			return new ShapeDirectSurface(isPositive, polygons);
		}
	}
}
