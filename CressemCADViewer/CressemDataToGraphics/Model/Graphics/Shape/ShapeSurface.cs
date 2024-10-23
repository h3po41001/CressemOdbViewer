using System.Collections.Generic;
using CressemExtractLibrary.Data.Interface.Features;
using ImageControl.Shape.Interface;

namespace CressemDataToGraphics.Model.Graphics.Shape
{
	internal class ShapeSurface : ShapeBase, IShapeSurface
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

		public IEnumerable<IShapePolygon> Polygons { get; private set; }

		public static ShapeSurface CreateGdiPlus(bool useMM, float pixelResolution,
			double xDatum, double yDatum,
			IFeatureSurface surface)
		{
			if (surface is null)
			{
				return null;
			}

			bool isPositive = surface.Polarity.Equals("P") is true;
			List<ShapePolygon> polygons = new List<ShapePolygon>();

			foreach (var polygon in surface.Polygons)
			{
				polygons.Add(ShapePolygon.CreateGdiPlus(useMM, pixelResolution,
					xDatum, yDatum, isPositive, polygon));
			}

			return new ShapeSurface(pixelResolution, 
				isPositive, polygons);
		}

		public static IShapeSurface CreateOpenGl(IFeatureSurface surface)
		{
			throw new System.NotImplementedException();
		}
	}
}
