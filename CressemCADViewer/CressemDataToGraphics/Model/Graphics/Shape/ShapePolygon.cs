using System.Collections.Generic;
using CressemExtractLibrary.Data.Interface.Features;
using ImageControl.Shape.Interface;

namespace CressemDataToGraphics.Model.Graphics.Shape
{
	internal class ShapePolygon : ShapeBase, IShapePolygon
	{
		private ShapePolygon() : base()
		{
		}

		public ShapePolygon(float pixelResolution,
			bool isFill, IEnumerable<ShapeBase> shapes) : base(pixelResolution)
		{
			IsFill = isFill;
			Shapes = shapes;
		}

		public bool IsFill { get; private set; }

		public IEnumerable<IShapeBase> Shapes { get; private set; }

		public static ShapePolygon CreateGdiPlus(bool useMM, float pixelResolution,
			bool isPositive, IFeaturePolygon polygon)
		{
			List<ShapeBase> shapes = new List<ShapeBase>();

			bool isIsland = polygon.PolygonType.Equals("I") is true;
			bool isFill = isPositive is true ? isIsland : !isIsland;

			foreach (var feature in polygon.Features)
			{
				if (feature is IFeatureArc arc)
				{
					shapes.Add(ShapeArc.CreateGdiPlus(useMM, pixelResolution, arc));
				}
				else if (feature is IFeatureBarcode barcode)
				{
					//shapes.Add(ShapeBarcode.CreateGdiPlus(pixelResolution, barcode));
				}
				else if (feature is IFeatureLine line)
				{
					shapes.Add(ShapeLine.CreateGdiPlus(useMM, pixelResolution, line));
				}
				else if (feature is IFeaturePad pad)
				{
					//shapes.Add(ShapePad.CreateGdiPlus(pixelResolution, pad));
				}
				else if (feature is IFeaturePolygon subPolygon)
				{
					shapes.Add(CreateGdiPlus(useMM, pixelResolution, isPositive, subPolygon));
				}
				else if (feature is IFeatureSurface surface)
				{
					shapes.Add(ShapeSurface.CreateGdiPlus(useMM, pixelResolution, surface));
				}
				else if (feature is IFeatureText textFeature)
				{
					//shapes.Add(ShapeText.CreateGdiPlus());
				}
			}

			return new ShapePolygon(pixelResolution, isFill, shapes);
		}

		public static IShapePolygon CreateOpenGl()
		{
			throw new System.NotImplementedException();
		}
	}
}
