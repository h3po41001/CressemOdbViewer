using System.Collections.Generic;
using System.Drawing;
using CressemDataToGraphics.Converter;
using CressemExtractLibrary.Data.Interface.Features;
using ImageControl.Shape.Gdi.Interface;
using ImageControl.Extension;

namespace CressemDataToGraphics.Model.Graphics.Shape
{
	internal class ShapeGdiPolygon : ShapeGdiBase, IGdiPolygon
	{
		private ShapeGdiPolygon() : base()
		{
		}

		public ShapeGdiPolygon(float pixelResolution,
			bool isFill,
			IEnumerable<ShapeGdiBase> shapes) : base(pixelResolution)
		{
			IsFill = isFill;
			Shapes = shapes;
		}

		public ShapeGdiPolygon(float pixelResolution,
			bool isFill,
			IEnumerable<PointF> points) : base(pixelResolution)
		{
			IsFill = isFill;
			Points = points;
		}

		public bool IsFill { get; private set; }

		public IEnumerable<IGdiBase> Shapes { get; private set; }

		public IEnumerable<PointF> Points { get; private set; }

		public static ShapeGdiPolygon CreateGdiPlus(bool useMM,
			float pixelResolution, bool isMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis,
			bool isPositive, IFeaturePolygon polygon)
		{
			List<ShapeGdiBase> shapes = new List<ShapeGdiBase>();

			bool isIsland = polygon.PolygonType.Equals("I") is true;
			bool isFill = isPositive is true ? isIsland : !isIsland;

			foreach (var feature in polygon.Features)
			{
				if (feature is IFeatureArc arc)
				{
					shapes.Add(ShapeGdiArc.Create(useMM,
						pixelResolution, isMM,
						xDatum + cx, yDatum + cy, polygon.X, polygon.Y,
						orient, isMirrorXAxis,
						arc.X, arc.Y, arc.Ex, arc.Ey, arc.Cx, arc.Cy,
						arc.IsClockWise, 0));
				}
				else if (feature is IFeatureLine line)
				{
					shapes.Add(ShapeLine.Create(useMM,
						pixelResolution, isMM,
						xDatum + cx, yDatum + cy, polygon.X, polygon.Y,
						polygon.Orient, polygon.IsMirrorXAxis,
						line.X, line.Y, line.Ex, line.Ey, 0));
				}
				else if (feature is IFeaturePolygon subPolygon)
				{
					shapes.Add(CreateGdiPlus(useMM,
						pixelResolution, isMM,
						xDatum + cx, yDatum + cy, polygon.X, polygon.Y,
						polygon.Orient, polygon.IsMirrorXAxis,
						isPositive, subPolygon));
				}
				else if (feature is IFeatureSurface surface)
				{
					shapes.Add(ShapeGdiSurface.CreateGdiPlus(useMM, 
						pixelResolution, isMM,
						xDatum + cx, yDatum + cy, polygon.X, polygon.Y,
						polygon.Orient, polygon.IsMirrorXAxis, isPositive, 
						surface.Polygons));
				}
			}

			return new ShapeGdiPolygon(pixelResolution, isFill, shapes);
		}

		public static ShapeGdiPolygon CreateGdiPlus(bool useMM, float pixelResolution,
			double xDatum, double yDatum, int orient, bool isMM, bool isPositive, string polygonType,
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

			return new ShapeGdiPolygon(pixelResolution, isFill, calcPoints);
		}

		public static IGdiPolygon CreateOpenGl()
		{
			throw new System.NotImplementedException();
		}
	}
}
