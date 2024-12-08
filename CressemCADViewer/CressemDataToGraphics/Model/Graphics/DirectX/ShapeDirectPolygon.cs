﻿using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CressemDataToGraphics.Factory;
using CressemExtractLibrary.Data.Interface.Features;
using ImageControl.Extension;
using ImageControl.Shape.DirectX.Interface;
using ImageControl.Shape.Interface;

namespace CressemDataToGraphics.Model.Graphics.DirectX
{
	internal class ShapeDirectPolygon : ShapeGraphicsBase, IDirectPolygon
	{

		private ShapeDirectPolygon() : base()
		{
		}

		public ShapeDirectPolygon(bool isFill,
			IEnumerable<IGraphicsShape> shapes) : base()
		{
			IsFill = isFill;
			Shapes = new List<IGraphicsShape>(shapes);
		}

		public ShapeDirectPolygon(bool isFill,
			IEnumerable<PointF> points) : base()
		{
			IsFill = isFill;
			Points = new List<PointF>(points.Select(x => new PointF(x.X, -x.Y)));
		}

		public bool IsFill { get; private set; }

		public IEnumerable<IGraphicsShape> Shapes { get; private set; }

		public IEnumerable<PointF> Points { get; private set; }

		public static ShapeDirectPolygon Create(bool useMM, float pixelResolution,
			int globalOrient, bool isGlobalFlipHorizontal, bool isMM,
			double datumX, double datumY,
			double cx, double cy,
			int orient, bool isFlipHorizontal,
			bool isPositive, IFeaturePolygon polygon)
		{
			List<ShapeGraphicsBase> shapes = new List<ShapeGraphicsBase>();

			bool isIsland = polygon.PolygonType.Equals("I") is true;
			bool isFill = isPositive is true ? isIsland : !isIsland;

			int localOrient = (orient + polygon.Orient) % 360;
			bool localIsFlipHorizontal = isFlipHorizontal ^ polygon.IsFlipHorizontal;

			foreach (var feature in polygon.Features)
			{
				if (feature is IFeatureArc arc)
				{
					shapes.Add(ShapeDirectArc.Create(useMM,
						pixelResolution, globalOrient, isGlobalFlipHorizontal,
						isMM, datumX, datumY,
						cx, cy,
						polygon.X, polygon.Y,
						localOrient, localIsFlipHorizontal,
						arc.X, arc.Y, arc.Ex, arc.Ey, arc.Cx, arc.Cy,
						arc.IsClockWise, 0));
				}
				else if (feature is IFeatureLine line)
				{
					shapes.Add(ShapeDirectLine.Create(useMM, pixelResolution,
						globalOrient, isGlobalFlipHorizontal,
						isMM, datumX, datumY,
						cx, cy,
						polygon.X, polygon.Y,
						localOrient, localIsFlipHorizontal,
						line.X, line.Y, line.Ex, line.Ey, 0));
				}
			}

			return new ShapeDirectPolygon(isFill, shapes);
		}

		public static ShapeDirectPolygon Create(bool useMM, float pixelResolution,
			int globalOrient, bool isGlobalFlipHorizontal,
			bool isMM, double datumX, double datumY,
			double cx, double cy,
			int orient, bool isFlipHorizontal,
			bool isPositive, string polygonType,
			IEnumerable<PointF> points)
		{
			var shapePolygon = ShapeFactory.Instance.CreatePolygon(useMM, pixelResolution,
				globalOrient, isGlobalFlipHorizontal,
				isMM, datumX, datumY,
				cx, cy,
				orient, isFlipHorizontal,
				isPositive, polygonType, points);

			return new ShapeDirectPolygon(shapePolygon.IsFill, shapePolygon.Points);
		}
	}
}
