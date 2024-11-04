using System.Collections.Generic;
using System.Drawing;
using CressemDataToGraphics.Factory;
using CressemExtractLibrary.Data.Interface.Features;
using ImageControl.Shape.Gdi.Interface;
using ImageControl.Shape.Interface;

namespace CressemDataToGraphics.Model.Graphics.Shape
{
	internal class ShapeGdiPolygon : ShapeGraphicsBase, IGdiPolygon
	{
		private ShapeGdiPolygon() : base()
		{
		}

		public ShapeGdiPolygon(bool isFill,
			IEnumerable<IGraphicsShape> shapes) : base()
		{
			IsFill = isFill;
			Shapes = shapes;
		}

		public ShapeGdiPolygon(bool isFill,
			IEnumerable<PointF> points) : base()
		{
			IsFill = isFill;
			Points = points;
		}

		public bool IsFill { get; private set; }

		public IEnumerable<IGraphicsShape> Shapes { get; private set; }

		public IEnumerable<PointF> Points { get; private set; }

		public static ShapeGdiPolygon Create(bool useMM,
			float pixelResolution, bool isMM,
			double datumX, double datumY, 
			double cx, double cy,
			int orient, bool isFlipHorizontal,
			bool isPositive, IFeaturePolygon polygon)
		{
			List<IGraphicsShape> shapes = new List<IGraphicsShape>();

			bool isIsland = polygon.PolygonType.Equals("I") is true;
			bool isFill = isPositive is true ? isIsland : !isIsland;

			foreach (var feature in polygon.Features)
			{
				if (feature is IFeatureArc arc)
				{
					shapes.Add(ShapeGdiArc.Create(useMM,
						pixelResolution, isMM,
						datumX + cx, datumY + cy,
						polygon.X, polygon.Y,
						orient, isFlipHorizontal,
						arc.X, arc.Y, arc.Ex, arc.Ey, arc.Cx, arc.Cy,
						arc.IsClockWise, 0));
				}
				else if (feature is IFeatureLine line)
				{
					shapes.Add(ShapeGdiLine.Create(useMM,
						pixelResolution, isMM,
						datumX + cx, datumY + cy, 
						polygon.X, polygon.Y,
						polygon.Orient, polygon.IsFlipHorizontal,
						line.X, line.Y, line.Ex, line.Ey, 0));
				}
				else if (feature is IFeaturePolygon subPolygon)
				{
					shapes.Add(Create(useMM,
						pixelResolution, isMM,
						datumX + cx, datumY + cy, 
						polygon.X, polygon.Y,
						polygon.Orient, polygon.IsFlipHorizontal,
						isPositive, subPolygon));
				}
				else if (feature is IFeatureSurface surface)
				{
					shapes.Add(ShapeGdiSurface.Create(useMM, 
						pixelResolution, isMM,
						datumX + cx, datumY + cy, 
						polygon.X, polygon.Y,
						polygon.Orient, polygon.IsFlipHorizontal, isPositive, 
						surface.Polygons));
				}
			}

			return new ShapeGdiPolygon(isFill, shapes);
		}

		public static ShapeGdiPolygon Create(bool useMM, 
			float pixelResolution, bool isMM,
			double datumX, double datumY,
			double cx, double cy,
			int orient, bool isFlipHorizontal, 
			bool isPositive, string polygonType,
			IEnumerable<PointF> points)
		{
			var shapePolygon = ShapeFactory.Instance.CreatePolygon(useMM, 
				pixelResolution, isMM,
				datumX, datumY, cx, cy,
				orient, isFlipHorizontal,
				isPositive, polygonType, points);

			return new ShapeGdiPolygon(shapePolygon.IsFill, shapePolygon.Points);
		}
	}
}
