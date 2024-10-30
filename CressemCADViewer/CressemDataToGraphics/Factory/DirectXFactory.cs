using System;
using System.Collections.Generic;
using System.Linq;
using CressemDataToGraphics.Model.Graphics.DirectX;
using CressemExtractLibrary.Data.Interface.Features;
using CressemExtractLibrary.Data.Interface.Symbol;
using ImageControl.Shape.DirectX.Interface;

namespace CressemDataToGraphics.Factory
{
	internal class DirectXFactory
	{
		public DirectXFactory() { }

		public IDirectList CreateFeatureToShape(bool useMM,
			float pixelResolution,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis, IFeatureBase feature)
		{
			if (feature is null)
			{
				return null;
			}

			return MakeFeatureShape(useMM, pixelResolution,
				feature.IsMM, xDatum, yDatum, cx, cy,
				orient, isMirrorXAxis, (dynamic)feature);
		}

		private IDirectList MakeFeatureShape(bool useMM,
			float pixelResolution, bool isMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis, IFeatureArc arc)
		{
			List<ShapeDirectBase> shapes = new List<ShapeDirectBase>();

			if (arc.FeatureSymbol is ISymbolRound symbolRound)
			{
				var startSymbol = MakeSymbolShape(useMM,
					pixelResolution, arc.IsMM,
					xDatum + cx, yDatum + cy, arc.X, arc.Y,
					orient, isMirrorXAxis,
					(dynamic)(arc.FeatureSymbol));

				var endSymbol = MakeSymbolShape(useMM,
					pixelResolution, arc.IsMM,
					xDatum + cx, yDatum + cy, arc.Ex, arc.Ey,
					orient, isMirrorXAxis,
					(dynamic)(arc.FeatureSymbol));

				shapes.Add(ShapeDirectArc.Create(useMM,
					pixelResolution, isMM,
					xDatum, yDatum, cx, cy,
					orient, isMirrorXAxis,
					arc.X, arc.Y, arc.Ex, arc.Ey, arc.Cx, arc.Cy,
					arc.IsClockWise, symbolRound.Diameter));

				shapes.Add(startSymbol);
				shapes.Add(endSymbol);
			}
			else
			{
				shapes.Add(ShapeDirectArc.Create(useMM,
					pixelResolution, isMM,
					xDatum, yDatum, cx, cy,
					orient, isMirrorXAxis,
					arc.X, arc.Y, arc.Ex, arc.Ey, arc.Cx, arc.Cy,
					arc.IsClockWise, 0));
			}

			return new ShapeDirectList(arc.Polarity.Equals("P"), shapes);
		}

		private IDirectList MakeFeatureShape(bool useMM,
			float pixelResolution, bool isMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis, IFeatureLine line)
		{
			List<ShapeDirectBase> shapes = new List<ShapeDirectBase>();

			if (line.FeatureSymbol is ISymbolIRegularShape regularShape)
			{
				var startSymbol = MakeSymbolShape(useMM,
					pixelResolution, line.IsMM,
					xDatum + cx, yDatum + cy, line.X, line.Y,
					orient, isMirrorXAxis,
					(dynamic)(line.FeatureSymbol));

				var endSymbol = MakeSymbolShape(useMM,
					pixelResolution, line.IsMM,
					xDatum + cx, yDatum + cy, line.Ex, line.Ey,
					orient, isMirrorXAxis,
					(dynamic)(line.FeatureSymbol));

				shapes.Add(ShapeDirectLine.Create(useMM,
					pixelResolution, isMM,
					xDatum, yDatum, cx, cy,
					orient, isMirrorXAxis,
					line.X, line.Y, line.Ex, line.Ey, regularShape.Diameter));

				shapes.Add(startSymbol);
				shapes.Add(endSymbol);
			}
			else
			{
				shapes.Add(ShapeDirectLine.Create(useMM,
					pixelResolution, isMM,
					xDatum, yDatum, cx, cy,
					orient, isMirrorXAxis,
					line.X, line.Y, line.Ex, line.Ey, 0));
			}

			return new ShapeDirectList(line.Polarity.Equals("P"), shapes);
		}

		private IDirectList MakeFeatureShape(bool useMM,
			float pixelResolution, bool isMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis, IFeatureBarcode barcode)
		{
			throw new System.NotImplementedException("바코드 아직 미구현");
		}

		private IDirectList MakeFeatureShape(bool useMM,
			float pixelResolution, bool isMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis, IFeaturePad pad)
		{
			List<IDirectShape> shapes = new List<IDirectShape>();

			if (pad.FeatureSymbol != null)
			{
				if (pad.FeatureSymbol is ISymbolUser userSymbol)
				{
					var userSymbolFeature = MakeUser(useMM,
						pixelResolution, isMM,
						xDatum + cx, yDatum + cy,
						pad.X, pad.Y,
						(pad.Orient + orient) % 360,
						isMirrorXAxis != pad.IsMirrorXAxis,
						userSymbol.FeaturesList);

					shapes.AddRange(userSymbolFeature);
				}
				else
				{
					shapes.Add(MakeSymbolShape(useMM,
						pixelResolution, isMM,
						xDatum + cx, yDatum + cy,
						pad.X, pad.Y,
						(pad.Orient + orient) % 360,
						isMirrorXAxis != pad.IsMirrorXAxis,
						pad.FeatureSymbol));
				}
			}

			return new ShapeDirectList(pad.Polarity.Equals("P"), shapes);
		}

		private IDirectList MakeFeatureShape(bool useMM,
			float pixelResolution, bool isMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis, IFeatureText text)
		{
			throw new System.NotImplementedException("텍스트 아직 미구현");
		}

		private IDirectList MakeFeatureShape(bool useMM,
			float pixelResolution, bool isMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis, IFeatureSurface surface)
		{
			List<ShapeDirectBase> shapes = new List<ShapeDirectBase>();

			bool isPositive = surface.Polarity.Equals("P") is true;
			shapes.Add(ShapeDirectSurface.Create(useMM,
				pixelResolution, isMM,
				xDatum, yDatum, cx, cy,
				orient, isMirrorXAxis,
				isPositive, surface.Polygons));

			return new ShapeDirectList(surface.Polarity.Equals("P"), shapes);
		}

		private IDirectList MakeFeatureShape(bool useMM,
			float pixelResolution, bool isMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis, IFeatureSurfaces surfaces)
		{

			List<ShapeDirectSurface> surfaceList = new List<ShapeDirectSurface>();

			foreach (var surface in surfaces.Features)
			{
				bool isPositive = surface.Polarity.Equals("P") is true;
				surfaceList.Add(ShapeDirectSurface.Create(useMM,
					pixelResolution, isMM,
					xDatum, yDatum, cx, cy,
					orient, isMirrorXAxis,
					isPositive, surface.Polygons));
			}

			return new ShapeDirectList(true, new ShapeDirectBase[] { new ShapeDirectSurfaces(surfaceList) });
		}

		private ShapeDirectBase MakeSymbolShape(bool useMM,
			float pixelResolution, bool isMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis,
			ISymbolBase symbol)
		{
			if (symbol is ISymbolRound round)
			{
				return ShapeDirectEllipse.Create(useMM,
					pixelResolution, isMM,
					xDatum, yDatum, cx, cy,
					orient, isMirrorXAxis,
					round.Diameter, round.Diameter);
			}
			else if (symbol is ISymbolSquare square)
			{
				return ShapeDirectRectangle.Create(useMM,
					pixelResolution, isMM,
					xDatum, yDatum, cx, cy,
					orient, isMirrorXAxis,
					square.Diameter, square.Diameter);
			}
			else if (symbol is ISymbolRectangle rectangle)
			{
				return ShapeDirectRectangle.Create(useMM,
					pixelResolution, isMM,
					xDatum, yDatum, cx, cy,
					orient, isMirrorXAxis,
					rectangle.Width, rectangle.Height);
			}
			else if (symbol is ISymbolEllipse ellipse)
			{
				return ShapeDirectEllipse.Create(useMM,
					pixelResolution, isMM,
					xDatum, yDatum, cx, cy,
					orient, isMirrorXAxis,
					ellipse.Width, ellipse.Height);
			}
			else if (symbol is ISymbolOval oval)
			{
				return ShapeDirectEllipse.Create(useMM,
					pixelResolution, isMM,
					xDatum, yDatum, cx, cy,
					orient, isMirrorXAxis,
					oval.Width, oval.Height);
			}
			else if (symbol is ISymbolRoundedRectangle roundedRectangle)
			{
				return MakeRoundedRectangle(useMM,
					pixelResolution, isMM,
					xDatum, yDatum, cx, cy,
					orient, isMirrorXAxis,
					roundedRectangle.Width, roundedRectangle.Height,
					roundedRectangle.CornerRadius,
					roundedRectangle.IsEditedCorner);
			}
			else if (symbol is ISymbolRoundDonut roundDonut)
			{
				return MakeRoundDonut(useMM,
					pixelResolution, isMM,
					xDatum, yDatum, cx, cy,
					orient, isMirrorXAxis,
					roundDonut.Diameter, roundDonut.InnerDiameter);
			}
			else if (symbol is ISymbolRoundThermalRounded roundThr)
			{
				return MakeRoundThermalRounded(useMM,
					pixelResolution, isMM,
					xDatum, yDatum, cx, cy,
					orient, isMirrorXAxis,
					roundThr.Diameter, roundThr.InnerDiameter,
					roundThr.Angle, roundThr.NumberOfSpoke, roundThr.Gap);
			}

			return null;
		}

		private ShapeDirectSurface MakeRoundedRectangle(bool useMM,
			float pixelResolution, bool isMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis,
			double width, double height, double radius, bool[] isEditedCorner)
		{
			double left = cx - width / 2;
			double right = cx + width / 2;
			double top = cy + height / 2;
			double bottom = cy - height / 2;

			List<ShapeDirectBase> shapeList = new List<ShapeDirectBase>();

			// RT
			if (isEditedCorner[0] is true)
			{
				shapeList.Add(ShapeDirectArc.Create(useMM,
					pixelResolution, isMM,
					xDatum, yDatum, cx, cy,
					orient, isMirrorXAxis,
					right - radius, top, // st
					right, top - radius, // ed
					right - radius, top - radius, // center
					true, 0));
			}
			else
			{
				shapeList.Add(ShapeDirectRectangle.Create(useMM,
					pixelResolution, isMM,
					xDatum, yDatum,
					right - radius / 2, top - radius / 2,
					orient, isMirrorXAxis, radius, radius));
			}

			// LT
			if (isEditedCorner[1] is true)
			{
				shapeList.Add(ShapeDirectArc.Create(useMM,
					pixelResolution, isMM,
					xDatum, yDatum, cx, cy,
					orient, isMirrorXAxis,
					left + radius, top, // st
					left, top - radius, // ed
					left + radius, top - radius, // center
					true, 0));
			}
			else
			{
				shapeList.Add(ShapeDirectRectangle.Create(useMM,
					pixelResolution, isMM,
					xDatum, yDatum,
					left + radius / 2, top - radius / 2,
					orient, isMirrorXAxis, radius, radius));
			}

			// LB
			if (isEditedCorner[2] is true)
			{
				shapeList.Add(ShapeDirectArc.Create(useMM,
						pixelResolution, isMM,
						xDatum, yDatum, cx, cy,
						orient, isMirrorXAxis,
						left + radius, bottom, // st
						left, bottom + radius, // ed
						left + radius, bottom + radius, // center
						true, 0));
			}
			else
			{
				shapeList.Add(ShapeDirectRectangle.Create(useMM,
					pixelResolution, isMM,
					xDatum, yDatum,
					left + radius / 2, bottom + radius / 2,
					orient, isMirrorXAxis, radius, radius));
			}

			// RB
			if (isEditedCorner[3] is true)
			{
				shapeList.Add(ShapeDirectArc.Create(useMM,
						pixelResolution, isMM,
						xDatum, yDatum, cx, cy,
						orient, isMirrorXAxis,
						right - radius, bottom, // st
						right, bottom + radius, // ed
						right - radius, bottom + radius, // center
						true, 0));
			}
			else
			{
				shapeList.Add(ShapeDirectRectangle.Create(useMM,
					pixelResolution, isMM,
					xDatum, yDatum,
					right - radius / 2, bottom + radius / 2,
					orient, isMirrorXAxis, radius, radius));
			}

			// Center Rectangle
			shapeList.Add(ShapeDirectRectangle.Create(
				useMM, pixelResolution, isMM,
				xDatum, yDatum, cx, cy,
				orient, isMirrorXAxis,
				width - radius * 2, height));

			shapeList.Add(ShapeDirectRectangle.Create(
				useMM, pixelResolution, isMM,
				xDatum, yDatum, cx, cy,
				orient, isMirrorXAxis,
				width, height - radius * 2));

			var polygon = new ShapeDirectPolygon(true, shapeList);
			return new ShapeDirectSurface(true, new ShapeDirectPolygon[] { polygon });
		}

		private ShapeDirectSurface MakeRoundDonut(bool useMM,
			float pixelResolution, bool isMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis,
			double outerDiameter, double innerDiameter)
		{
			var outerCircle = ShapeDirectEllipse.Create(useMM,
				pixelResolution, isMM,
				xDatum, yDatum, cx, cy,
				orient, isMirrorXAxis,
				outerDiameter, outerDiameter);

			var innerCircle = ShapeDirectEllipse.Create(useMM,
				pixelResolution, isMM,
				xDatum, yDatum, cx, cy,
				orient, isMirrorXAxis,
				innerDiameter, innerDiameter);

			//var outerPoly = new ShapeDirectPolygon(true, new ShapeDirectBase[] { outerCircle });
			//var innerPoly = new ShapeDirectPolygon(false, new ShapeDirectBase[] { innerCircle });
			var polys = new ShapeDirectPolygon(false, new ShapeDirectBase[] { outerCircle, innerCircle });

			return new ShapeDirectSurface(true, new ShapeDirectPolygon[] { polys });
		}

		private ShapeDirectSurface MakeRoundThermalRounded(bool useMM,
			float pixelResolution, bool isMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis,
			double outDiameter, double innerDiameter,
			double angle, int spokeNum, double gap)
		{
			double ringWidth = outDiameter - innerDiameter;
			double radius = innerDiameter / 2 + ringWidth / 4;
			double gapDegree = angle * 180 / Math.PI;
			double oneOfDeg = (2 * Math.Asin(ringWidth / (8 * radius))) * 180.0 / Math.PI;
			double pieceOfDeg = 360.0 / angle;

			double radian = Math.PI / 180.0;
			for (int i = 0; i < spokeNum; i++)
			{
				// 시작점 계산
				double sX = radius * Math.Cos((angle + gapDegree / 2.0 + oneOfDeg / 2.0 + pieceOfDeg * i) * radian);
				double sY = radius * Math.Sin((angle + gapDegree / 2.0 + oneOfDeg / 2.0 + pieceOfDeg * i) * radian);

				// 끝점 계산
				double eX = radius * Math.Cos((angle + gapDegree / 2.0 + oneOfDeg / 2.0 + pieceOfDeg * i + pieceOfDeg - gapDegree - oneOfDeg) * radian);
				double eY = radius * Math.Sin((angle + gapDegree / 2.0 + oneOfDeg / 2.0 + pieceOfDeg * i + pieceOfDeg - gapDegree - oneOfDeg) * radian);

				//ShapeDirectArc.Create(useMM, pixelResolution, isMM, xDatum, yDatum, cx, cy, orient, isMirrorXAxis,
				//	sX, sY, eX, eY, cx, cy, 
			}


			return null;
		}

		private IEnumerable<IDirectShape> MakeUser(bool useMM,
			float pixelResolution, bool isMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis,
			IEnumerable<IFeatureBase> features)
		{
			List<IDirectShape> shapeList = new List<IDirectShape>();
			foreach (var feature in features)
			{
				ShapeDirectList featureShapes = MakeFeatureShape(useMM,
					pixelResolution, isMM,
					xDatum + cx, yDatum + cy, feature.X, feature.Y,
					orient, isMirrorXAxis, (dynamic)feature);

				shapeList.AddRange(featureShapes.Shapes);
			}

			return shapeList;
		}
	}
}
