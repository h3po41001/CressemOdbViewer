using System;
using System.Collections.Generic;
using System.Drawing;
using CressemDataToGraphics.Converter;
using CressemDataToGraphics.Factory;
using CressemExtractLibrary.Data.Interface.Features;
using ImageControl.Extension;
using ImageControl.Shape.DirectX.Interface;

namespace CressemDataToGraphics.Model.Graphics.DirectX
{
	internal class ShapeDirectPolygon : ShapeDirectBase, IDirectPolygon
	{

		private ShapeDirectPolygon() : base()
		{
		}

		public ShapeDirectPolygon(bool isFill,
			IEnumerable<ShapeDirectBase> shapes) : base()
		{
			IsFill = isFill;
			Shapes = shapes;
		}

		public ShapeDirectPolygon(bool isFill,
			IEnumerable<PointF> points) : base()
		{
			IsFill = isFill;
			Points = points;
		}

		public bool IsFill { get; private set; }

		public IEnumerable<IDirectShape> Shapes { get; private set; }

		public IEnumerable<PointF> Points { get; private set; }

		public static ShapeDirectPolygon Create(bool useMM,
			float pixelResolution, bool isMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis,
			bool isPositive, IFeaturePolygon polygon)
		{
			List<ShapeDirectBase> shapes = new List<ShapeDirectBase>();

			bool isIsland = polygon.PolygonType.Equals("I") is true;
			bool isFill = isPositive is true ? isIsland : !isIsland;

			foreach (var feature in polygon.Features)
			{
				if (feature is IFeatureArc arc)
				{
					shapes.Add(ShapeDirectArc.Create(useMM,
						pixelResolution, isMM,
						xDatum + cx, yDatum + cy, polygon.X, polygon.Y,
						orient, isMirrorXAxis,
						arc.X, arc.Y, arc.Ex, arc.Ey, arc.Cx, arc.Cy,
						arc.IsClockWise, 0));
				}
				else if (feature is IFeatureLine line)
				{
					shapes.Add(ShapeDirectLine.Create(useMM,
						pixelResolution, isMM,
						xDatum + cx, yDatum + cy, polygon.X, polygon.Y,
						polygon.Orient, polygon.IsMirrorXAxis,
						line.X, line.Y, line.Ex, line.Ey, 0));
				}
				else if (feature is IFeaturePolygon subPolygon)
				{
					shapes.Add(Create(useMM,
						pixelResolution, isMM,
						xDatum + cx, yDatum + cy, polygon.X, polygon.Y,
						polygon.Orient, polygon.IsMirrorXAxis,
						isPositive, subPolygon));
				}
				else if (feature is IFeatureSurface surface)
				{
					shapes.Add(ShapeDirectSurface.Create(useMM,
						pixelResolution, isMM,
						xDatum + cx, yDatum + cy, polygon.X, polygon.Y,
						polygon.Orient, polygon.IsMirrorXAxis, isPositive,
						surface.Polygons));
				}
			}

			return new ShapeDirectPolygon(isFill, shapes);
		}

		public static ShapeDirectPolygon Create(bool useMM, 
			float pixelResolution, bool isMM,
			double xDatum, double yDatum,  double cx, double cy,
			int orient, bool isMirrorXAxis,
			bool isPositive, string polygonType,
			IEnumerable<PointF> points)
		{
			var shapePolygon = ShapeFactory.Instance.CreatePolygon(useMM,
				pixelResolution, isMM, xDatum, yDatum, cx, cy, orient, isMirrorXAxis,
				isPositive, polygonType, points);

			return new ShapeDirectPolygon(shapePolygon.IsFill, shapePolygon.Points);
		}
	}
}
