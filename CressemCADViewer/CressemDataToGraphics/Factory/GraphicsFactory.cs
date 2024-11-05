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

		public abstract IGraphicsShape CreateArc(bool useMM, float pixelResolution,
			int globalOrient, bool isGlobalFlipHorizontal,
			bool isMM, double datumX, double datumY,
			double anchorX, double anchorY,
			double cx, double cy,
			int orient, bool isFlipHorizontal,
			double sx, double sy,
			double ex, double ey,
			double arcCx, double arcCy,
			bool isClockWise, double width);

		public abstract IGraphicsShape CreateLine(bool useMM, float pixelResolution,
			int globalOrient, bool isGlobalFlipHorizontal,
			bool isMM, double datumX, double datumY,
			double anchorX, double anchorY,
			double cx, double cy,
			int orient, bool isFlipHorizontal,
			double sx, double sy,
			double ex, double ey, double width);

		public abstract IGraphicsShape CreateSurface(bool useMM, float pixelResolution,
			int globalOrient, bool isGlobalFlipHorizontal, bool isMM,
			double datumX, double datumY,
			double cx, double cy,
			int orient, bool isFlipHorizontal,
			bool isPositive, IEnumerable<IFeaturePolygon> featurePolygons);

		public abstract IGraphicsShape CreateSurface(bool isPositive,
			IEnumerable<IGraphicsShape> shapes);

		public abstract IGraphicsShape CreateEllipse(bool useMM, float pixelResolution,
			int globalOrient, bool isGlobalFlipHorizontal, bool isMM,
			double datumX, double datumY,
			double cx, double cy,
			int orient, bool isFlipHorizontal,
			double width, double height);

		public abstract IGraphicsShape CreateRectangle(bool useMM, float pixelResolution,
			int globalOrient, bool isGlobalFlipHorizontal, bool isMM,
			double datumX, double datumY,
			double cx, double cy,
			int orient, bool isFlipHorizontal,
			double width, double height);

		public abstract IGraphicsShape CreatePolygon(bool isFill,
			IEnumerable<IGraphicsShape> shapes);

		public IGraphicsList CreateFeatureToShape(bool useMM, float pixelResolution,
			int globalOrient, bool isGlobalFlipHorizontal,
			double datumX, double datumY, double cx, double cy,
			int orient, bool isFlipHorizontal, IFeatureBase feature)
		{
			if (feature is null)
			{
				return null;
			}

			return MakeFeatureShape(useMM, pixelResolution,
				globalOrient, isGlobalFlipHorizontal,
				feature.IsMM, datumX, datumY, cx, cy,
				orient, isFlipHorizontal, (dynamic)feature);
		}

		private IGraphicsList MakeFeatureShape(bool useMM, float pixelResolution,
			int globalOrient, bool isGlobalFlipHorizontal, bool isMM,
			double datumX, double datumY, double cx, double cy,
			int orient, bool isFlipHorizontal, IFeatureArc arc)
		{
			List<IGraphicsShape> shapes = new List<IGraphicsShape>();

			double width = 0;
			if (arc.FeatureSymbol is ISymbolRound symbolRound)
			{
				var startSymbol = MakeSymbolShape(useMM, pixelResolution,
					globalOrient, isGlobalFlipHorizontal,
					arc.IsMM, datumX, datumY,
					arc.X, arc.Y,
					orient, isFlipHorizontal,
					(dynamic)(arc.FeatureSymbol));

				var endSymbol = MakeSymbolShape(useMM, pixelResolution,
					globalOrient, isGlobalFlipHorizontal,
					arc.IsMM, datumX, datumY,
					arc.Ex, arc.Ey,
					orient, isFlipHorizontal,
					(dynamic)(arc.FeatureSymbol));

				width = symbolRound.Diameter;

				shapes.Add(startSymbol);
				shapes.Add(endSymbol);
			}

			var arcShape = CreateArc(useMM, pixelResolution,
				globalOrient, isGlobalFlipHorizontal,
				isMM, datumX, datumY, cx, cy, cx, cy,
				orient, isFlipHorizontal,
				arc.X, arc.Y, arc.Ex, arc.Ey, arc.Cx, arc.Cy,
				arc.IsClockWise, width);

			shapes.Add(arcShape);

			return new ShapeGraphicsList(arc.Polarity.Equals("P"), shapes);
		}

		private IGraphicsList MakeFeatureShape(bool useMM, float pixelResolution,
			int globalOrient, bool isGlobalFlipHorizontal,
			bool isMM, double datumX, double datumY,
			double cx, double cy,
			int orient, bool isFlipHorizontal, IFeatureLine line)
		{
			List<IGraphicsShape> shapes = new List<IGraphicsShape>();

			double width = 0;
			if (line.FeatureSymbol is ISymbolIRegularShape regularShape)
			{
				var startSymbol = MakeSymbolShape(useMM, pixelResolution,
					globalOrient, isGlobalFlipHorizontal,
					line.IsMM, datumX, datumY,
					line.X, line.Y,
					orient, isFlipHorizontal,
					(dynamic)(line.FeatureSymbol));

				var endSymbol = MakeSymbolShape(useMM, pixelResolution,
					globalOrient, isGlobalFlipHorizontal,
					line.IsMM, datumX, datumY,
					line.Ex, line.Ey,
					orient, isFlipHorizontal,
					(dynamic)(line.FeatureSymbol));

				width = regularShape.Diameter;

				shapes.Add(startSymbol);
				shapes.Add(endSymbol);
			}

			var lineShape = CreateLine(useMM, pixelResolution,
				globalOrient, isGlobalFlipHorizontal, isMM,
				datumX, datumY, cx, cy, cx, cy,
				orient, isFlipHorizontal,
				line.X, line.Y, line.Ex, line.Ey, width);

			shapes.Add(lineShape);

			return new ShapeGraphicsList(line.Polarity.Equals("P"), shapes);
		}

		private IGraphicsList MakeFeatureShape(bool useMM, float pixelResolution,
			int globalOrient, bool isGlobalFlipHorizontal,
			bool isMM, double datumX, double datumY,
			double cx, double cy,
			int orient, bool isFlipHorizontal, IFeatureBarcode barcode)
		{
			//throw new System.NotImplementedException("바코드 아직 미구현");
			return null;
		}

		private IGraphicsList MakeFeatureShape(bool useMM, float pixelResolution,
			int globalOrient, bool isGlobalFlipHorizontal,
			bool isMM, double datumX, double datumY,
			double cx, double cy,
			int orient, bool isFlipHorizontal, IFeaturePad pad)
		{
			List<IGraphicsShape> shapes = new List<IGraphicsShape>();

			if (pad.FeatureSymbol != null)
			{
				if (pad.FeatureSymbol is ISymbolUser userSymbol)
				{
					var userSymbolFeature = MakeUser(useMM, pixelResolution,
						globalOrient, isGlobalFlipHorizontal,
						isMM, datumX, datumY,
						pad.X, pad.Y,
						(pad.Orient + orient) % 360,
						isFlipHorizontal != pad.IsFlipHorizontal,
						userSymbol.FeaturesList);

					shapes.AddRange(userSymbolFeature);
				}
				else
				{
					var symbol = MakeSymbolShape(useMM, pixelResolution,
						globalOrient, isGlobalFlipHorizontal,
						isMM, datumX, datumY,
						pad.X, pad.Y,
						(pad.Orient + orient) % 360,
						isFlipHorizontal != pad.IsFlipHorizontal,
						pad.FeatureSymbol);

					shapes.Add(symbol);
				}
			}

			return new ShapeGraphicsList(pad.Polarity.Equals("P"), shapes);
		}

		private IGraphicsList MakeFeatureShape(bool useMM, float pixelResolution,
			int globalOrient, bool isGlobalFlipHorizontal,
			bool isMM, double datumX, double datumY,
			double cx, double cy,
			int orient, bool isFlipHorizontal, IFeatureText text)
		{
			//throw new System.NotImplementedException("텍스트 아직 미구현");
			return null;
		}

		private IGraphicsList MakeFeatureShape(bool useMM, float pixelResolution,
			int globalOrient, bool isGlobalFlipHorizontal,
			bool isMM, double datumX, double datumY,
			double cx, double cy,
			int orient, bool isFlipHorizontal, IFeatureSurface surface)
		{
			List<IGraphicsShape> shapes = new List<IGraphicsShape>();

			bool isPositive = surface.Polarity.Equals("P") is true;

			var surfaceShape = CreateSurface(useMM, pixelResolution,
				globalOrient, isGlobalFlipHorizontal,
				isMM, datumX, datumY,
				cx, cy,
				orient, isFlipHorizontal,
				isPositive, surface.Polygons);

			shapes.Add(surfaceShape);

			return new ShapeGraphicsList(surface.Polarity.Equals("P"), shapes);
		}

		private IGraphicsShape MakeSymbolShape(bool useMM, float pixelResolution,
			int globalOrient, bool isGlobalFlipHorizontal,
			bool isMM, double datumX, double datumY,
			double cx, double cy,
			int orient, bool isFlipHorizontal,
			ISymbolBase symbol)
		{
			if (symbol is ISymbolRound round)
			{
				return CreateEllipse(useMM, pixelResolution,
					globalOrient, isGlobalFlipHorizontal,
					isMM, datumX, datumY,
					cx, cy,
					orient, isFlipHorizontal,
					round.Diameter, round.Diameter);
			}
			else if (symbol is ISymbolSquare square)
			{
				return CreateRectangle(useMM, pixelResolution,
					globalOrient, isGlobalFlipHorizontal,
					isMM, datumX, datumY,
					cx, cy,
					orient, isFlipHorizontal,
					square.Diameter, square.Diameter);
			}
			else if (symbol is ISymbolRectangle rectangle)
			{
				return CreateRectangle(useMM, pixelResolution,
					globalOrient, isGlobalFlipHorizontal,
					isMM, datumX, datumY,
					cx, cy,
					orient, isFlipHorizontal,
					rectangle.Width, rectangle.Height);
			}
			else if (symbol is ISymbolEllipse ellipse)
			{
				return CreateEllipse(useMM, pixelResolution,
					globalOrient, isGlobalFlipHorizontal,
					isMM, datumX, datumY,
					cx, cy,
					orient, isFlipHorizontal,
					ellipse.Width, ellipse.Height);
			}
			else if (symbol is ISymbolOval oval)
			{
				return CreateEllipse(useMM, pixelResolution,
					globalOrient, isGlobalFlipHorizontal,
					isMM, datumX, datumY,
					cx, cy,
					orient, isFlipHorizontal,
					oval.Width, oval.Height);
			}
			else if (symbol is ISymbolRoundedRectangle roundedRectangle)
			{
				return MakeRoundedRectangle(useMM, pixelResolution,
					globalOrient, isGlobalFlipHorizontal,
					isMM, datumX, datumY,
					cx, cy,
					orient, isFlipHorizontal,
					roundedRectangle.Width, roundedRectangle.Height,
					roundedRectangle.CornerRadius,
					roundedRectangle.IsEditedCorner);
			}
			else if (symbol is ISymbolRoundDonut roundDonut)
			{
				return MakeRoundDonut(useMM, pixelResolution,
					globalOrient, isGlobalFlipHorizontal,
					isMM, datumX, datumY,
					cx, cy,
					orient, isFlipHorizontal,
					roundDonut.Diameter, roundDonut.InnerDiameter);
			}
			else if (symbol is ISymbolRoundThermalRounded roundThr)
			{
				return MakeRoundThermalRounded(useMM, pixelResolution,
					globalOrient, isGlobalFlipHorizontal,
					isMM, datumX, datumY,
					cx, cy,
					orient, isFlipHorizontal,
					roundThr.Diameter, roundThr.InnerDiameter,
					roundThr.Angle, roundThr.NumberOfSpoke, roundThr.Gap);
			}

			return null;
		}

		private IGraphicsShape MakeRoundedRectangle(bool useMM, float pixelResolution,
			int globalOrient, bool isGlobalFlipHorizontal,
			bool isMM, double datumX, double datumY,
			double cx, double cy,
			int orient, bool isFlipHorizontal,
			double width, double height, double radius, bool[] isEditedCorner)
		{
			throw new System.NotImplementedException("RoundedRectangle 아직 미구현");
		}

		private IGraphicsShape MakeRoundDonut(bool useMM, float pixelResolution,
			int globalOrient, bool isGlobalFlipHorizontal,
			bool isMM, double datumX, double datumY,
			double cx, double cy,
			int orient, bool isFlipHorizontal,
			double outerDiameter, double innerDiameter)
		{
			var outerCircle = CreateEllipse(useMM, pixelResolution,
				globalOrient, isGlobalFlipHorizontal, isMM,
				datumX, datumY, cx, cy,
				orient, isFlipHorizontal,
				outerDiameter, outerDiameter);

			var innerCircle = CreateEllipse(useMM, pixelResolution,
				globalOrient, isGlobalFlipHorizontal, isMM,
				datumX, datumY, cx, cy,
				orient, isFlipHorizontal,
				innerDiameter, innerDiameter);

			var outer = CreatePolygon(true, new IGraphicsShape[] { outerCircle });
			var inner = CreatePolygon(false, new IGraphicsShape[] { innerCircle });

			return CreateSurface(true, new IGraphicsShape[] { outer, inner });
		}

		private IGraphicsShape MakeRoundThermalRounded(bool useMM, float pixelResolution,
			int globalOrient, bool isGlobalFlipHorizontal,
			bool isMM, double datumX, double datumY,
			double cx, double cy,
			int orient, bool isFlipHorizontal,
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

			//	//ShapeDirectArc.Create(useMM, pixelResolution, isMM, datumX, datumY, cx, cy, orient, isFlipHorizontal,
			//	//	sX, sY, eX, eY, cx, cy, 
			//}

			//throw new System.NotImplementedException("RoundThermalRounded 아직 미구현");
			return null;
		}

		private IEnumerable<IGraphicsShape> MakeUser(bool useMM, float pixelResolution,
			int globalOrient, bool isGlobalFlipHorizontal,
			bool isMM, double datumX, double datumY,
			double cx, double cy,
			int orient, bool isFlipHorizontal,
			IEnumerable<IFeatureBase> features)
		{
			List<IGraphicsShape> shapeList = new List<IGraphicsShape>();
			foreach (var feature in features)
			{
				IGraphicsList featureShapes = MakeFeatureShape(useMM, pixelResolution,
					globalOrient, isGlobalFlipHorizontal,
					isMM, datumX + cx, datumY + cy,
					feature.X, feature.Y,
					orient, isFlipHorizontal, (dynamic)feature);

				shapeList.AddRange(featureShapes.Shapes);
			}

			return shapeList;
		}
	}
}
