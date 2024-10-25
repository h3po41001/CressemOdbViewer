using System.Collections.Generic;
using CressemExtractLibrary.Data.Interface.Features;
using ImageControl.Shape.Gdi.Interface;

namespace CressemDataToGraphics.Model.Graphics.Shape
{
	internal class ShapeSurface : ShapeBase, IGdiSurface
	{
		private ShapeSurface() : base()
		{
		}

		public ShapeSurface(float pixelResolution,
			bool isPositive,
			IEnumerable<ShapePolygon> polygons) : base(pixelResolution)
		{
			IsPositive = isPositive;
			Polygons = polygons;
		}

		public bool IsPositive { get; private set; }

		public IEnumerable<IGdiPolygon> Polygons { get; private set; }

		public static ShapeSurface CreateGdiPlus(bool useMM,
			float pixelResolution, bool isMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis, 
			bool isPositive, IEnumerable<IFeaturePolygon> featurePolygons)
		{
			if (featurePolygons is null)
			{
				return null;
			}

			List<ShapePolygon> polygons = new List<ShapePolygon>();

			foreach (var polygon in featurePolygons)
			{
				polygons.Add(ShapePolygon.CreateGdiPlus(useMM,
					pixelResolution, isMM,
					xDatum, yDatum, cx, cy,
					orient, isMirrorXAxis, isPositive, polygon));
			}

			return new ShapeSurface(pixelResolution,
				isPositive, polygons);
		}

		public static IGdiSurface CreateOpenGl(IFeatureSurface surface)
		{
			throw new System.NotImplementedException();
		}
	}
}
