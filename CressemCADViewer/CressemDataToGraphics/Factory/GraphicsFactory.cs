using System.Collections.Generic;
using CressemDataToGraphics.Model.Graphics;
using CressemExtractLibrary.Data.Interface.Features;
using CressemExtractLibrary.Data.Interface.Symbol;
using ImageControl.Shape.Interface;

namespace CressemDataToGraphics.Factory
{
	internal abstract class GraphicsFactory
	{
		protected GraphicsFactory() { }

		public abstract IGraphicsShape CreateArc(bool useMM,
			float pixelResolution, bool isMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis,
			double sx, double sy,
			double ex, double ey,
			double arcCx, double arcCy,
			bool isClockWise, double width);

		public abstract IGraphicsShape CreateLine(bool useMM,
			float pixelResolution, bool isMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis,
			double sx, double sy, 
			double ex, double ey, double width);

		public abstract IGraphicsShape CreateSurface(bool useMM,
			float pixelResolution, bool isMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis,
			bool isPositive, IEnumerable<IFeaturePolygon> featurePolygons);

		public abstract IGraphicsShape CreateSurface(bool isPositive, 
			IEnumerable<IGraphicsShape> shapes);

		public abstract IGraphicsShape CreateEllipse(bool useMM,
			float pixelResolution, bool isMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis,
			double width, double height);

		public abstract IGraphicsShape CreateRectangle(bool useMM,
			float pixelResolution, bool isMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis,
			double width, double height);

		public abstract IGraphicsShape CreatePolygon(bool isFill,
			IEnumerable<IGraphicsShape> shapes);

		public IGraphicsList CreateFeatureToShape(bool useMM,
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

		private IGraphicsList MakeFeatureShape(bool useMM,
			float pixelResolution, bool isMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis, IFeatureArc arc)
		{
			List<IGraphicsShape> shapes = new List<IGraphicsShape>();

			double width = 0;
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

				width = symbolRound.Diameter;

				shapes.Add(startSymbol);
				shapes.Add(endSymbol);
			}

			var arcShape = CreateArc(useMM,
				pixelResolution, isMM,
				xDatum, yDatum, cx, cy,
				orient, isMirrorXAxis,
				arc.X, arc.Y, arc.Ex, arc.Ey, arc.Cx, arc.Cy,
				arc.IsClockWise, width);

			shapes.Add(arcShape);

			return new ShapeGraphicsList(arc.Polarity.Equals("P"), shapes);
		}

		private IGraphicsList MakeFeatureShape(bool useMM,
			float pixelResolution, bool isMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis, IFeatureLine line)
		{
			List<IGraphicsShape> shapes = new List<IGraphicsShape>();

			double width = 0;
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

				width = regularShape.Diameter;

				shapes.Add(startSymbol);
				shapes.Add(endSymbol);
			}

			var lineShape = CreateLine(useMM,
				pixelResolution, isMM,
				xDatum, yDatum, cx, cy,
				orient, isMirrorXAxis,
				line.X, line.Y, line.Ex, line.Ey, width);

			shapes.Add(lineShape);

			return new ShapeGraphicsList(line.Polarity.Equals("P"), shapes);
		}

		private IGraphicsList MakeFeatureShape(bool useMM,
			float pixelResolution, bool isMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis, IFeatureBarcode barcode)
		{
			throw new System.NotImplementedException("바코드 아직 미구현");
		}

		private IGraphicsList MakeFeatureShape(bool useMM,
			float pixelResolution, bool isMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis, IFeaturePad pad)
		{
			List<IGraphicsShape> shapes = new List<IGraphicsShape>();

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
					var symbol = MakeSymbolShape(useMM,
						pixelResolution, isMM,
						xDatum + cx, yDatum + cy,
						pad.X, pad.Y,
						(pad.Orient + orient) % 360,
						isMirrorXAxis != pad.IsMirrorXAxis,
						pad.FeatureSymbol);

					shapes.Add(symbol);
				}
			}

			return new ShapeGraphicsList(pad.Polarity.Equals("P"), shapes);
		}

		private IGraphicsList MakeFeatureShape(bool useMM,
			float pixelResolution, bool isMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis, IFeatureText text)
		{
			throw new System.NotImplementedException("텍스트 아직 미구현");
		}

		private IGraphicsList MakeFeatureShape(bool useMM,
			float pixelResolution, bool isMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis, IFeatureSurface surface)
		{
			List<IGraphicsShape> shapes = new List<IGraphicsShape>();

			bool isPositive = surface.Polarity.Equals("P") is true;

			var surfaceShape = CreateSurface(useMM,
				pixelResolution, isMM,
				xDatum, yDatum, cx, cy,
				orient, isMirrorXAxis,
				isPositive, surface.Polygons);

			shapes.Add(surfaceShape);

			return new ShapeGraphicsList(surface.Polarity.Equals("P"), shapes);
		}

		private IGraphicsShape MakeSymbolShape(bool useMM,
			float pixelResolution, bool isMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis,
			ISymbolBase symbol)
		{
			if (symbol is ISymbolRound round)
			{
				return CreateEllipse(useMM,
					pixelResolution, isMM,
					xDatum, yDatum, cx, cy,
					orient, isMirrorXAxis,
					round.Diameter, round.Diameter);
			}
			else if (symbol is ISymbolSquare square)
			{
				return CreateRectangle(useMM,
					pixelResolution, isMM,
					xDatum, yDatum, cx, cy,
					orient, isMirrorXAxis,
					square.Diameter, square.Diameter);
			}
			else if (symbol is ISymbolRectangle rectangle)
			{
				return CreateRectangle(useMM,
					pixelResolution, isMM,
					xDatum, yDatum, cx, cy,
					orient, isMirrorXAxis,
					rectangle.Width, rectangle.Height);
			}
			else if (symbol is ISymbolEllipse ellipse)
			{
				return CreateEllipse(useMM,
					pixelResolution, isMM,
					xDatum, yDatum, cx, cy,
					orient, isMirrorXAxis,
					ellipse.Width, ellipse.Height);
			}
			else if (symbol is ISymbolOval oval)
			{
				return CreateEllipse(useMM,
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

		private IGraphicsShape MakeRoundedRectangle(bool useMM,
			float pixelResolution, bool isMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis,
			double width, double height, double radius, bool[] isEditedCorner)
		{
			throw new System.NotImplementedException("RoundedRectangle 아직 미구현");
		}

		private IGraphicsShape MakeRoundDonut(bool useMM,
			float pixelResolution, bool isMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis,
			double outerDiameter, double innerDiameter)
		{
			var outerCircle = CreateEllipse(useMM,
				pixelResolution, isMM,
				xDatum, yDatum, cx, cy,
				orient, isMirrorXAxis,
				outerDiameter, outerDiameter);

			var innerCircle = CreateEllipse(useMM,
				pixelResolution, isMM,
				xDatum, yDatum, cx, cy,
				orient, isMirrorXAxis,
				innerDiameter, innerDiameter);

			var outer = CreatePolygon(true, new IGraphicsShape[] { outerCircle });
			var inner = CreatePolygon(false, new IGraphicsShape[] { innerCircle });

			return CreateSurface(true, new IGraphicsShape[] {outer, inner});
		}

		private IGraphicsShape MakeRoundThermalRounded(bool useMM,
			float pixelResolution, bool isMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis,
			double outDiameter, double innerDiameter,
			double angle, int spokeNum, double gap)
		{
			//double ringWidth = outDiameter - innerDiameter;
			//double radius = innerDiameter / 2 + ringWidth / 4;
			//double gapDegree = angle * 180 / Math.PI;
			//double oneOfDeg = (2 * Math.Asin(ringWidth / (8 * radius))) * 180.0 / Math.PI;
			//double pieceOfDeg = 360.0 / angle;

			//double radian = Math.PI / 180.0;
			//for (int i = 0; i < spokeNum; i++)
			//{
			//	// 시작점 계산
			//	double sX = radius * Math.Cos((angle + gapDegree / 2.0 + oneOfDeg / 2.0 + pieceOfDeg * i) * radian);
			//	double sY = radius * Math.Sin((angle + gapDegree / 2.0 + oneOfDeg / 2.0 + pieceOfDeg * i) * radian);

			//	// 끝점 계산
			//	double eX = radius * Math.Cos((angle + gapDegree / 2.0 + oneOfDeg / 2.0 + pieceOfDeg * i + pieceOfDeg - gapDegree - oneOfDeg) * radian);
			//	double eY = radius * Math.Sin((angle + gapDegree / 2.0 + oneOfDeg / 2.0 + pieceOfDeg * i + pieceOfDeg - gapDegree - oneOfDeg) * radian);

			//	//ShapeDirectArc.Create(useMM, pixelResolution, isMM, xDatum, yDatum, cx, cy, orient, isMirrorXAxis,
			//	//	sX, sY, eX, eY, cx, cy, 
			//}

			throw new System.NotImplementedException("RoundThermalRounded 아직 미구현");
		}

		private IEnumerable<IGraphicsShape> MakeUser(bool useMM,
			float pixelResolution, bool isMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis,
			IEnumerable<IFeatureBase> features)
		{
			List<IGraphicsShape> shapeList = new List<IGraphicsShape>();
			foreach (var feature in features)
			{
				IGraphicsList featureShapes = MakeFeatureShape(useMM,
					pixelResolution, isMM,
					xDatum + cx, yDatum + cy, feature.X, feature.Y,
					orient, isMirrorXAxis, (dynamic)feature);

				shapeList.AddRange(featureShapes.Shapes);
			}

			return shapeList;
		}
	}
}
