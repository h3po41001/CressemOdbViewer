using System.Collections.Generic;
using CressemExtractLibrary.Data.Interface.Features;
using ImageControl.Shape.DirectX.Interface;

namespace CressemDataToGraphics.Model.Graphics.DirectX
{
	internal class ShapeDirectSurface : ShapeDirectBase, IDirectSurface
	{
		private ShapeDirectSurface() : base()
		{
		}

		public ShapeDirectSurface(bool isPositive,
			IEnumerable<ShapeDirectPolygon> polygons) : base()
		{
			IsPositive = isPositive;
			Polygons = polygons;
		}

		public bool IsPositive { get; private set; }

		public IEnumerable<IDirectPolygon> Polygons { get; private set; }

		public static ShapeDirectSurface Create(bool useMM,
			float pixelResolution, bool isMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis,
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
					xDatum, yDatum, cx, cy,
					orient, isMirrorXAxis, isPositive, polygon));
			}

			return new ShapeDirectSurface(isPositive, polygons);
		}
	}
}
