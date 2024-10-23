using CressemDataToGraphics.Model.Graphics.Shape;
using CressemExtractLibrary.Data.Interface.Features;
using CressemExtractLibrary.Data.Interface.Symbol;
using ImageControl.Shape.Interface;

namespace CressemDataToGraphics.Factory
{
	internal class GdiPlusFactory
	{
		public GdiPlusFactory()
		{
		}

		public IShapeList CreateFeatureToShape(bool useMM, float pixelResolution,
			double xDatum, double yDatum, IFeatureBase feature)
		{
			if (feature is null)
			{
				return null;
			}

			if (feature is IFeaturePolygon)
			{
				return null;
			}

			return MakeFeatureShape(useMM, pixelResolution, xDatum, yDatum, (dynamic)feature);
		}

		private IShapeList MakeFeatureShape(bool useMM, float pixelResolution,
			double xDatum, double yDatum, IFeatureArc arc)
		{
			ShapeList shapes = new ShapeList();

			if (arc.FeatureSymbol is ISymbolRound symbolRound)
			{
				var startSymbol = MakeSymbolShape(useMM, pixelResolution, arc.IsMM,
					arc.X, arc.Y, (dynamic)(arc.FeatureSymbol));

				var endSymbol = MakeSymbolShape(useMM, pixelResolution, arc.IsMM,
					arc.Ex, arc.Ey, (dynamic)(arc.FeatureSymbol));

				shapes.AddShape(ShapeArc.CreateGdiPlus(useMM, pixelResolution, xDatum, yDatum,
					symbolRound.Diameter, arc));

				shapes.AddShape(startSymbol);
				shapes.AddShape(endSymbol);
			}
			else
			{
				shapes.AddShape(ShapeArc.CreateGdiPlus(useMM, pixelResolution, xDatum, yDatum, 0, arc));
			}

			return shapes;

		}

		private IShapeList MakeFeatureShape(bool useMM, float pixelResolution,
			double xDatum, double yDatum, IFeatureBarcode barcode)
		{
			throw new System.NotImplementedException("바코드 아직 미구현");
		}

		private IShapeList MakeFeatureShape(bool useMM, float pixelResolution,
			double xDatum, double yDatum, IFeatureLine line)
		{
			ShapeList shapes = new ShapeList();

			if (line.FeatureSymbol is ISymbolRound symbolRound)
			{
				var startSymbol = MakeSymbolShape(useMM, pixelResolution, line.IsMM,
					line.X, line.Y, (dynamic)(line.FeatureSymbol));

				var endSymbol = MakeSymbolShape(useMM, pixelResolution, line.IsMM,
					line.Ex, line.Ey, (dynamic)(line.FeatureSymbol));

				shapes.AddShape(ShapeLine.CreateGdiPlus(useMM, pixelResolution,
					xDatum, yDatum, symbolRound.Diameter, line));

				shapes.AddShape(startSymbol);
				shapes.AddShape(endSymbol);
			}
			else
			{
				shapes.AddShape(ShapeLine.CreateGdiPlus(useMM, pixelResolution, xDatum, yDatum, 0, line));
			}

			return shapes;
		}

		private IShapeList MakeFeatureShape(bool useMM, float pixelResolution,
			double xDatum, double yDatum, IFeaturePad pad)
		{
			ShapeList shapes = new ShapeList();
			if (pad.FeatureSymbol is ISymbolRectangle rect)
			{
				shapes.AddShape(MakeSymbolShape(useMM, pixelResolution, xDatum, yDatum, pad.OrientDef,
					pad.IsMM, pad.X, pad.Y, rect));
			}

			return shapes;
		}

		private IShapeList MakeFeatureShape(bool useMM, float pixelResolution,
			double xDatum, double yDatum, IFeatureSurface surface)
		{
			ShapeList shapes = new ShapeList();
			shapes.AddShape(ShapeSurface.CreateGdiPlus(
				useMM, pixelResolution, xDatum, yDatum, surface));

			if (surface.FeatureSymbol != null)
			{
				shapes.AddShape(MakeSymbolShape(useMM, pixelResolution, surface.IsMM,
					 xDatum, yDatum, surface.X, surface.Y, 0, (dynamic)(surface.FeatureSymbol)));
			}

			return shapes;
		}

		// Round
		private IShapeBase MakeSymbolShape(bool useMM, float pixelResolution,
			bool isMM, double cx, double cy,
			ISymbolRound round)
		{
			return ShapeEllipse.CreateGdiPlus(useMM, pixelResolution,
				isMM, cx, cy, round.Diameter, round.Diameter);
		}

		// Square
		private IShapeBase MakeSymbolShape(bool useMM, float pixelResolution,
			bool isMM, double cx, double cy,
			ISymbolSquare square)
		{
			return null;
			//return ShapeRectangle.CreateGdiPlus(useMM, pixelResolution,
			//	isMM, cx, cy, square.Diameter, square.Diameter);
		}

		// Rectangle
		private IShapeBase MakeSymbolShape(bool useMM, float pixelResolution,
			double xDatum, double yDatum, int orient,
			bool isMM, double cx, double cy,
			ISymbolRectangle rect)
		{
			return ShapeRectangle.CreateGdiPlus(useMM, pixelResolution,
				xDatum, yDatum, orient,
				isMM, cx, cy, rect.Width, rect.Height);
		}

		// Rounded Rectangle
		private IShapeBase MakeSymbolShape(bool useMM, float pixelResolution,
			bool isMM, double cx, double cy,
			ISymbolRoundedRectangle rect)
		{
			return null;
			//float sx = (float)cx;
			//float sy = (float)cy;
			//float fWidth = (float)rect.Width;
			//float fHeight = (float)rect.Height;
			//float radius = (float)rect.CornerRadius;

			//if (useMM is true)
			//{
			//	if (isMM is false)
			//	{
			//		sx = (float)cx.ConvertInchToMM();
			//		sy = (float)cy.ConvertInchToMM();
			//		fWidth = (float)rect.Width.ConvertInchToUM();
			//		fHeight = (float)rect.Height.ConvertInchToUM();
			//		radius = (float)rect.CornerRadius.ConvertInchToUM();
			//	}
			//}
			//else
			//{
			//	if (isMM is true)
			//	{
			//		sx = (float)cx.ConvertMMToInch();
			//		sy = (float)cy.ConvertMMToInch();
			//		fWidth = (float)rect.Width.ConvertUMToInch();
			//		fHeight = (float)rect.Height.ConvertUMToInch();
			//		radius = (float)rect.CornerRadius.ConvertUMToInch();
			//	}
			//}

			//// Gdi에 그릴때는 LT부터 width, height 만큼 그림
			//// ODB에서 LT 좌표는 (sx - fWidth / 2), (sy + fHeight / 2) 
			//float cornerSx = sx - fWidth / 2;
			//float cornerSy = sy + fHeight / 2;
			//float cornerWidth = radius;
			//float cornerHeight = radius;

			//List<ShapeArc> arcList = new List<ShapeArc>();
			//// RT
			//if (rect.IsEditedCorner[0] is true)
			//{
			//	cornerSx += (fWidth - radius);
			//	arcList.Add(new ShapeArc(pixelResolution, cornerSx, -cornerSy,
			//		cornerWidth, cornerHeight, 270, 90, 0));
			//}

			//// LT
			//if (rect.IsEditedCorner[1] is true)
			//{
			//	arcList.Add(new ShapeArc(pixelResolution, cornerSx, -cornerSy,
			//		cornerWidth, cornerHeight, 180, 90, 0));
			//}

			//// LB
			//if (rect.IsEditedCorner[2] is true)
			//{
			//	cornerSy += (-fHeight + radius);
			//	arcList.Add(new ShapeArc(pixelResolution, cornerSx, -cornerSy,
			//		cornerWidth, cornerHeight, 90, 90, 0));
			//}

			//// RB
			//if (rect.IsEditedCorner[3] is true)
			//{
			//	cornerSx += (fWidth - radius);
			//	cornerSy += (-fHeight + radius);
			//	arcList.Add(new ShapeArc(pixelResolution, cornerSx, -cornerSy,
			//		cornerWidth, cornerHeight, 0, 90, 0));
			//}

			//return new ShapePolygon(pixelResolution, true, arcList);
		}

		private IShapeBase MakeSymbolShape(bool useMM, float pixelResolution,
			bool isMM, double cx, double cy, ISymbolButterfly butterfly)
		{
			return null;
			//float sx = (float)cx;
			//float sy = (float)cy;
			//float width = (float)butterfly.Diameter;
			//float height = (float)butterfly.Diameter;

			//if (useMM is true)
			//{
			//	if (isMM is false)
			//	{
			//		sx = (float)cx.ConvertInchToMM();
			//		sy = (float)cy.ConvertInchToMM();
			//		width = (float)butterfly.Diameter.ConvertInchToUM();
			//		height = (float)butterfly.Diameter.ConvertInchToUM();
			//	}
			//}
			//else
			//{
			//	if (isMM is true)
			//	{
			//		sx = (float)cx.ConvertMMToInch();
			//		sy = (float)cy.ConvertMMToInch();
			//		width = (float)butterfly.Diameter.ConvertUMToInch();
			//		height = (float)butterfly.Diameter.ConvertUMToInch();
			//	}
			//}

			//// Gdi에 그릴때는 LT부터 width, height 만큼 그림
			//// ODB에서 LT 좌표는 (sx - width / 2), (sy + height / 2) 
			//// 하지만 Gdi는 y좌표가 반대이므로 -1곱한다
			//var leftTop = new ShapeArc(pixelResolution,
			//	(sx - width / 2), -(sy + height / 2), width / 2, height / 2, 180, 90, 0);

			//var rightBottom = new ShapeArc(pixelResolution,
			//	sx, -sy, width / 2, height / 2, 270, 90, 0);

			//return new ShapePolygon(pixelResolution, true,
			//	new ShapeBase[] { leftTop, rightBottom });
		}

		private IShapeBase MakeSymbolShape(bool useMM, float pixelResolution,
			bool isMM, double cx, double cy, ISymbolChamferedRectangle rect)
		{
			return null;
		}

		private IShapeBase MakeSymbolShape(bool useMM, float pixelResolution,
			bool isMM, double cx, double cy,
			ISymbolDiamond diamond)
		{
			return null;
			//float fx = (float)cx;
			//float fy = (float)cy;
			//float fWidth = (float)diamond.Width;
			//float fHeight = (float)diamond.Height;

			//if (useMM is true)
			//{
			//	if (isMM is false)
			//	{
			//		fx = (float)cx.ConvertInchToMM();
			//		fy = (float)cy.ConvertInchToMM();
			//		fWidth = (float)diamond.Width.ConvertInchToUM();
			//		fHeight = (float)diamond.Height.ConvertInchToUM();
			//	}
			//}
			//else
			//{
			//	if (isMM is true)
			//	{
			//		fx = (float)cx.ConvertMMToInch();
			//		fy = (float)cy.ConvertMMToInch();
			//		fWidth = (float)diamond.Width.ConvertUMToInch();
			//		fHeight = (float)diamond.Height.ConvertUMToInch();
			//	}
			//}

			//var topRight = new ShapeLine(pixelResolution,
			//	fx, -(fy + fHeight / 2), fx + fWidth / 2, -fy);
			//var topLeft = new ShapeLine(pixelResolution,
			//	fx, -(fy + fHeight / 2), fx - fWidth / 2, -fy);
			//var bottomRight = new ShapeLine(pixelResolution,
			//	fx, -(fy - fHeight / 2), fx + fWidth / 2, -fy);
			//var bottomLeft = new ShapeLine(pixelResolution,
			//	fx, -(fy - fHeight / 2), fx - fWidth / 2, -fy);

			//return new ShapePolygon(pixelResolution, true,
			//	new ShapeBase[] { topRight, topLeft, bottomRight, bottomLeft });
		}

		private IShapeBase MakeSymbolShape(bool useMM, float pixelResolution,
			bool isMM, double cx, double cy,
			ISymbolEditedCorner corner)
		{
			return null;
		}

		private IShapeBase MakeSymbolShape(bool useMM, float pixelResolution,
			bool isMM, double cx, double cy,
			ISymbolEllipse ellipse)
		{
			return ShapeEllipse.CreateGdiPlus(useMM, pixelResolution,
				isMM, cx, cy, ellipse.Width, ellipse.Height);
		}

		private IShapeBase MakeSymbolShape(bool useMM, float pixelResolution,
			bool isMM, double cx, double cy,
			 ISymbolHalfOval halfOval)
		{
			return null;
		}

		private IShapeBase MakeSymbolShape(bool useMM, float pixelResolution,
			bool isMM, double cx, double cy,
			ISymbolHole hole)
		{
			return ShapeEllipse.CreateGdiPlus(useMM, pixelResolution,
				isMM, cx, cy, hole.Diameter, hole.Diameter);
		}

		private IShapeBase MakeSymbolShape(bool useMM, float pixelResolution,
			bool isMM, double cx, double cy,
			ISymbolHorizontalHexagon hexagon)
		{
			return null;
		}

		private IShapeBase MakeSymbolShape(bool useMM, float pixelResolution,
			bool isMM, double cx, double cy,
			ISymbolMoire moire)
		{
			return null;
		}

		private IShapeBase MakeSymbolShape(bool useMM, float pixelResolution,
			bool isMM, double cx, double cy,
			ISymbolOctagon octagon)
		{
			return null;
		}

		private IShapeBase MakeSymbolShape(bool useMM, float pixelResolution,
			bool isMM, double cx, double cy,
			ISymbolOval oval)
		{
			return null;
		}

		private IShapeBase MakeSymbolShape(bool useMM, float pixelResolution,
			bool isMM, double cx, double cy,
			ISymbolOvalDonut ovalDonut)
		{
			return null;
		}

		private IShapeBase MakeSymbolShape(bool useMM, float pixelResolution,
			bool isMM, double cx, double cy,
			ISymbolOvalThermal ovalThermal)
		{
			return null;
		}

		private IShapeBase MakeSymbolShape(bool useMM, float pixelResolution,
			bool isMM, double cx, double cy,
			ISymbolOvalThermalOpenCorners ovalThermalOpenCorners)
		{
			return null;
		}

		private IShapeBase MakeSymbolShape(bool useMM, float pixelResolution,
			bool isMM, double cx, double cy,
			ISymbolRectangleDonut rectangleDonut)
		{
			return null;
		}

		private IShapeBase MakeSymbolShape(bool useMM, float pixelResolution,
			bool isMM, double cx, double cy,
			ISymbolRectangularThermal rectangularThermal)
		{
			return null;
		}

		private IShapeBase MakeSymbolShape(bool useMM, float pixelResolution,
			bool isMM, double cx, double cy,
			ISymbolRectangularThermalOpenCorners rectangularThermalOpenCorners)
		{
			return null;
		}

		private IShapeBase MakeSymbolShape(bool useMM, float pixelResolution,
			bool isMM, double cx, double cy,
			ISymbolRoundDonut roundDonut)
		{
			var outerCircle = ShapeEllipse.CreateGdiPlus(useMM, pixelResolution,
				isMM, cx, cy, roundDonut.Diameter, roundDonut.Diameter);

			var innerCircle = ShapeEllipse.CreateGdiPlus(useMM, pixelResolution,
				isMM, cx, cy, roundDonut.InnerDiameter, roundDonut.InnerDiameter);

			var outerPoly = new ShapePolygon(pixelResolution, true,
				new ShapeBase[] { outerCircle });

			var innerPoly = new ShapePolygon(pixelResolution, false,
				new ShapeBase[] { innerCircle });

			return new ShapeSurface(pixelResolution, true,
				new ShapePolygon[] { outerPoly, innerPoly });
		}

		private IShapeBase MakeSymbolShape(bool useMM, float pixelResolution,
			bool isMM, double cx, double cy,
			ISymbolRoundedRectangleDonut roundedRectangleDonut)
		{
			return null;
		}

		private IShapeBase MakeSymbolShape(bool useMM, float pixelResolution,
			bool isMM, double cx, double cy,
			ISymbolRoundedRectangleThermal roundedRectangleThermal)
		{
			return null;
		}

		private IShapeBase MakeSymbolShape(bool useMM, float pixelResolution,
			bool isMM, double cx, double cy,
			ISymbolRoundedRectangleThermalOpenCorners roundedRectangleThermalOpenCorners)
		{
			return null;
		}

		private IShapeBase MakeSymbolShape(bool useMM, float pixelResolution,
			bool isMM, double cx, double cy,
			ISymbolRoundedSquareThermal roundedSquareThermal)
		{
			return null;
		}

		private IShapeBase MakeSymbolShape(bool useMM, float pixelResolution,
			bool isMM, double cx, double cy,
			ISymbolRoundedSquareThermalOpenCorners roundedSquareThermalOpenCorners)
		{
			return null;
		}

		private IShapeBase MakeSymbolShape(bool useMM, float pixelResolution,
			bool isMM, double cx, double cy,
			ISymbolRoundedSqureDonut roundedSqureDonut)
		{
			return null;
		}

		private IShapeBase MakeSymbolShape(bool useMM, float pixelResolution,
			bool isMM, double cx, double cy,
			ISymbolRoundThermalRounded roundThermalRounded)
		{
			return null;
		}

		private IShapeBase MakeSymbolShape(bool useMM, float pixelResolution,
			bool isMM, double cx, double cy,
			ISymbolRoundThermalSquared roundThermalSquared)
		{
			return null;
		}

		private IShapeBase MakeSymbolShape(bool useMM, float pixelResolution,
			bool isMM, double cx, double cy,
			ISymbolSquareButterfly squareButterfly)
		{
			return null;
			//double halfDiameter = squareButterfly.Diameter / 2;

			//var leftTop = ShapeRectangle.CreateGdiPlus(useMM, pixelResolution, isMM,
			//	cx - halfDiameter, cy - halfDiameter, halfDiameter, halfDiameter);

			//var rightBottom = ShapeRectangle.CreateGdiPlus(useMM, pixelResolution, isMM,
			//	cx, cy, halfDiameter, halfDiameter);

			//return new ShapePolygon(pixelResolution, true,
			//	new ShapeBase[] { leftTop, rightBottom });
		}

		private IShapeBase MakeSymbolShape(bool useMM, float pixelResolution,
			bool isMM, double cx, double cy,
			ISymbolSquareDonut squareDonut)
		{
			return null;
		}

		private IShapeBase MakeSymbolShape(bool useMM, float pixelResolution,
			bool isMM, double cx, double cy,
			ISymbolSquareRoundDonut squareRoundDonut)
		{
			return null;
		}

		private IShapeBase MakeSymbolShape(bool useMM, float pixelResolution,
			bool isMM, double cx, double cy,
			ISymbolSquareRoundThermal squareRoundThermal)
		{
			return null;
		}

		private IShapeBase MakeSymbolShape(bool useMM, float pixelResolution,
			bool isMM, double cx, double cy,
			ISymbolSquareThermal squareThermal)
		{
			return null;
		}

		private IShapeBase MakeSymbolShape(bool useMM, float pixelResolution,
			bool isMM, double cx, double cy,
			ISymbolSquareThermalOpenCorners squareThermalOpenCorners)
		{
			return null;
		}

		private IShapeBase MakeSymbolShape(bool useMM, float pixelResolution,
			bool isMM, double cx, double cy,
			ISymbolTriangle triangle)
		{
			return null;
		}

		private IShapeList MakeSymbolShape(bool useMM, float pixelResolution,
			bool isMM, double cx, double cy,
			ISymbolUser user)
		{
			return null;
		}

		private IShapeBase MakeSymbolShape(bool useMM, float pixelResolution,
			bool isMM, double xDatum, double yDatum, double cx, double cy,
			int orient, ISymbolVerticalHexagon verticalHexagon)
		{
			return null;
		}
	}
}
