using System.Collections.Generic;
using System.Drawing;
using CressemDataToGraphics.Converter;
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
			float xDatum, float yDatum, int orient,
			bool isFill,
			IEnumerable<ShapeBase> shapes) : base(pixelResolution, xDatum, yDatum, orient)
		{
			IsFill = isFill;
			Shapes = shapes;
		}

		public ShapePolygon(float pixelResolution,
			float xDatum, float yDatum, int orient,
			bool isFill, 
			IEnumerable<PointF> points) : base(pixelResolution, xDatum, yDatum, orient)
		{
			IsFill = isFill;
			Points = points;
		}

		public bool IsFill { get; private set; }

		public IEnumerable<IShapeBase> Shapes { get; private set; }

		public IEnumerable<PointF> Points { get; private set; }

		public static ShapePolygon CreateGdiPlus(bool useMM, float pixelResolution,
			double xDatum, double yDatum, bool isPositive, IFeaturePolygon polygon)
		{
			List<ShapeBase> shapes = new List<ShapeBase>();

			bool isIsland = polygon.PolygonType.Equals("I") is true;
			bool isFill = isPositive is true ? isIsland : !isIsland;

			foreach (var feature in polygon.Features)
			{
				if (feature is IFeatureArc arc)
				{
					shapes.Add(ShapeArc.CreateGdiPlus(useMM, pixelResolution, xDatum, yDatum, 0, arc));
				}
				else if (feature is IFeatureLine line)
				{
					shapes.Add(ShapeLine.CreateGdiPlus(useMM, pixelResolution, xDatum, yDatum, 0, line));
				}
				else if (feature is IFeaturePolygon subPolygon)
				{
					shapes.Add(CreateGdiPlus(useMM, pixelResolution, xDatum, yDatum, isPositive, subPolygon));
				}
				else if (feature is IFeatureSurface surface)
				{
					shapes.Add(ShapeSurface.CreateGdiPlus(useMM, pixelResolution, xDatum, yDatum, surface));
				}
			}

			return new ShapePolygon(pixelResolution, (float)xDatum, (float)yDatum, 0, isFill, shapes);
		}

		public static ShapePolygon CreateGdiPlus(bool useMM, float pixelResolution,
			double xDatum, double yDatum, bool isMM, bool isPositive, string polygonType,
			IEnumerable<PointF> points)
		{
			List<PointF> calcPoints = new List<PointF>();
			foreach (var point in points)
			{
				double x = point.X + xDatum;
				double y = point.Y + yDatum;

				if (useMM is true)
				{
					if (isMM is false)
					{
						x = x.ConvertInchToMM();
						y = y.ConvertInchToMM();
					}
				}
				else
				{
					if (isMM is true)
					{
						x = x.ConvertMMToInch();
						y = y.ConvertMMToInch();
					}
				}

				calcPoints.Add(new PointF((float)x, (float)-y));
			}

			bool isIsland = polygonType.Equals("I") is true;
			bool isFill = isPositive is true ? isIsland : !isIsland;

			return new ShapePolygon(pixelResolution, isFill, calcPoints);
		}

		public static IShapePolygon CreateOpenGl()
		{
			throw new System.NotImplementedException();
		}
	}
}
