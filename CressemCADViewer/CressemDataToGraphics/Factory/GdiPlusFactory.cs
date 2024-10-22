using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using CressemDataToGraphics.Converter;
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

		public IShapeBase CreateFeatureToShape(bool useMM,
			float pixelResolution, IFeatureBase feature)
		{
			if (feature is IFeatureArc arc)
			{
				return ShapeArc.CreateGdiPlus(useMM, pixelResolution, arc);
			}
			else if (feature is IFeatureBarcode barcode)
			{
				//return ShapeBarcode.CreateGdiPlus(pixelResolution, barcode);
			}
			else if (feature is IFeatureLine line)
			{
				return ShapeLine.CreateGdiPlus(useMM, pixelResolution, line);
			}
			else if (feature is IFeaturePolygon polygon)
			{
				return ShapePolygon.CreateGdiPlus(useMM, pixelResolution, true, polygon);
			}
			else if (feature is IFeatureSurface surface)
			{
				return ShapeSurface.CreateGdiPlus(useMM, pixelResolution, surface);
			}
			else if (feature is IFeatureText text)
			{
				//return ShapeText.CreateGdiPlus(pixelResolution, text);
			}

			return null;
		}

		public IShapeBase CreateSymbolToShape(bool useMM, float pixelResolution,
			bool isMM, double cx, double cy, ISymbolBase symbol)
		{
			if (symbol is null)
			{
				return null;
			}

			return MakeShape(useMM, pixelResolution, isMM, cx, cy, (dynamic)symbol);
		}

		// Round
		private IShapeBase MakeShape(bool useMM, float pixelResolution,
			bool isMM, double cx, double cy, ISymbolRound round)
		{
			return MakeShapeEllipse(useMM, pixelResolution,
				isMM, cx, cy, round.Diameter, round.Diameter);
		}

		// Square
		private IShapeBase MakeShape(bool useMM, float pixelResolution,
			bool isMM, double cx, double cy, ISymbolSquare square)
		{
			return MakeShapeRectangle(useMM, pixelResolution,
				isMM, cx, cy, square.OuterDiameter, square.OuterDiameter);
		}

		// Rectangle
		private IShapeBase MakeShape(bool useMM, float pixelResolution,
			bool isMM, double cx, double cy, ISymbolRectangle rect)
		{
			return MakeShapeRectangle(useMM, pixelResolution,
				isMM, cx, cy, rect.Width, rect.Height);
		}

		// Rounded Rectangle
		private IShapeBase MakeShape(bool useMM, float pixelResolution,
			bool isMM, double cx, double cy, ISymbolRoundedRectangle rect)
		{
			float sx = (float)cx;
			float sy = (float)cy;
			float fWidth = (float)rect.Width;
			float fHeight = (float)rect.Height;
			float radius = (float)rect.CornerRadius;

			if (useMM is true)
			{
				if (isMM is false)
				{
					sx = (float)cx.ConvertInchToMM();
					sy = (float)cy.ConvertInchToMM();
					fWidth = (float)rect.Width.ConvertInchToUM();
					fHeight = (float)rect.Height.ConvertInchToUM();
					radius = (float)rect.CornerRadius.ConvertInchToUM();
				}
			}
			else
			{
				if (isMM is true)
				{
					sx = (float)cx.ConvertMMToInch();
					sy = (float)cy.ConvertMMToInch();
					fWidth = (float)rect.Width.ConvertUMToInch();
					fHeight = (float)rect.Height.ConvertUMToInch();
					radius = (float)rect.CornerRadius.ConvertUMToInch();
				}
			}

			// Gdi에 그릴때는 LT부터 width, height 만큼 그림
			// ODB에서 LT 좌표는 (sx - fWidth / 2), (sy + fHeight / 2) 
			float cornerSx = sx - fWidth / 2;
			float cornerSy = sy + fHeight / 2;
			float cornerWidth = radius;
			float cornerHeight = radius;

			List<ShapeArc> arcList = new List<ShapeArc>();
			// RT
			if (rect.IsEditedCorner[0] is true)
			{
				cornerSx += (fWidth - radius);
				arcList.Add(new ShapeArc(pixelResolution, cornerSx, -cornerSy, cornerWidth, cornerHeight, 270, 90));
			}

			// LT
			if (rect.IsEditedCorner[1] is true)
			{
				arcList.Add(new ShapeArc(pixelResolution, cornerSx, -cornerSy, cornerWidth, cornerHeight, 180, 90));
			}

			// LB
			if (rect.IsEditedCorner[2] is true)
			{
				cornerSy += (-fHeight + radius);
				arcList.Add(new ShapeArc(pixelResolution, cornerSx, -cornerSy, cornerWidth, cornerHeight, 90, 90));
			}

			// RB
			if (rect.IsEditedCorner[3] is true)
			{
				cornerSx += (fWidth - radius);
				cornerSy += (-fHeight + radius);
				arcList.Add(new ShapeArc(pixelResolution, cornerSx, -cornerSy, cornerWidth, cornerHeight, 0, 90));
			}

			return new ShapePolygon(pixelResolution, true, arcList);
		}

		private IShapeBase MakeShape(bool useMM, float pixelResolution,
			bool isMM, double cx, double cy, ISymbolButterfly butterfly)
		{
			float sx = (float)cx;
			float sy = (float)cy;
			float width = (float)butterfly.Diameter;
			float height = (float)butterfly.Diameter;

			if (useMM is true)
			{
				if (isMM is false)
				{
					sx = (float)cx.ConvertInchToMM();
					sy = (float)cy.ConvertInchToMM();
					width = (float)butterfly.Diameter.ConvertInchToUM();
					height = (float)butterfly.Diameter.ConvertInchToUM();
				}
			}
			else
			{
				if (isMM is true)
				{
					sx = (float)cx.ConvertMMToInch();
					sy = (float)cy.ConvertMMToInch();
					width = (float)butterfly.Diameter.ConvertUMToInch();
					height = (float)butterfly.Diameter.ConvertUMToInch();
				}
			}

			// Gdi에 그릴때는 LT부터 width, height 만큼 그림
			// ODB에서 LT 좌표는 (sx - width / 2), (sy + height / 2) 
			// 하지만 Gdi는 y좌표가 반대이므로 -1곱한다
			var leftTop = new ShapeArc(pixelResolution, (sx - width / 2), -(sy + height / 2), width / 2, height / 2, 180, 90);
			var rightBottom = new ShapeArc(pixelResolution, sx, -sy, width / 2, height / 2, 270, 90);

			return new ShapePolygon(pixelResolution, true, new ShapeBase[] { leftTop, rightBottom });
		}

		private IShapeBase MakeShape(ISymbolChamferedRectangle rect)
		{
			return null;
		}

		private IShapeBase MakeShape(bool useMM, float pixelResolution,
			bool isMM, double cx, double cy, ISymbolDiamond diamond)
		{
			float fx = (float)cx;
			float fy = (float)cy;
			float fWidth = (float)diamond.Width;
			float fHeight = (float)diamond.Height;

			if (useMM is true)
			{
				if (isMM is false)
				{
					fx = (float)cx.ConvertInchToMM();
					fy = (float)cy.ConvertInchToMM();
					fWidth = (float)diamond.Width.ConvertInchToUM();
					fHeight = (float)diamond.Height.ConvertInchToUM();
				}
			}
			else
			{
				if (isMM is true)
				{
					fx = (float)cx.ConvertMMToInch();
					fy = (float)cy.ConvertMMToInch();
					fWidth = (float)diamond.Width.ConvertUMToInch();
					fHeight = (float)diamond.Height.ConvertUMToInch();
				}
			}

			var topRight = new ShapeLine(pixelResolution,
				fx, -(fy + fHeight / 2), fx + fWidth / 2, -fy);
			var topLeft = new ShapeLine(pixelResolution,
				fx, -(fy + fHeight / 2), fx - fWidth / 2, -fy);
			var bottomRight = new ShapeLine(pixelResolution,
				fx, -(fy - fHeight / 2), fx + fWidth / 2, -fy);
			var bottomLeft = new ShapeLine(pixelResolution,
				fx, -(fy - fHeight / 2), fx - fWidth / 2, -fy);

			return new ShapePolygon(pixelResolution, true, new ShapeBase[] { topRight, topLeft, bottomRight, bottomLeft });
		}

		private IShapeBase MakeShape(ISymbolEditedCorner corner)
		{
			return null;
		}

		private IShapeBase MakeShape(bool useMM, float pixelResolution,
			bool isMM, double x, double y, ISymbolEllipse ellipse)
		{
			return MakeShapeEllipse(useMM, pixelResolution,
				isMM, x, y, ellipse.Width, ellipse.Height);
		}

		private IShapeBase MakeShape(ISymbolHalfOval halfOval)
		{
			return null;
		}

		private IShapeBase MakeShape(bool useMM, float pixelResolution,
			bool isMM, double x, double y, ISymbolHole hole)
		{
			return MakeShapeEllipse(useMM, pixelResolution,
				isMM, x, y, hole.Diameter, hole.Diameter);
		}

		private IShapeBase MakeShape(ISymbolHorizontalHexagon hexagon)
		{
			return null;
		}

		private IShapeBase MakeShape(ISymbolMoire moire)
		{
			return null;
		}

		private IShapeBase MakeShape(ISymbolOctagon octagon)
		{
			return null;
		}

		private IShapeBase MakeShape(ISymbolOval oval)
		{
			return null;
		}

		private IShapeBase MakeShape(ISymbolOvalDonut ovalDonut)
		{
			return null;
		}

		private IShapeBase MakeShape(ISymbolOvalThermal ovalThermal)
		{
			return null;
		}

		private IShapeBase MakeShape(ISymbolOvalThermalOpenCorners ovalThermalOpenCorners)
		{
			return null;
		}

		private IShapeBase MakeShape(ISymbolRectangleDonut rectangleDonut)
		{
			return null;
		}

		private IShapeBase MakeShape(ISymbolRectangularThermal rectangularThermal)
		{
			return null;
		}

		private IShapeBase MakeShape(ISymbolRectangularThermalOpenCorners rectangularThermalOpenCorners)
		{
			return null;
		}

		private IShapeBase MakeShape(ISymbolRoundDonut roundDonut)
		{
			return null;
		}

		private IShapeBase MakeShape(ISymbolRoundedRectangleDonut roundedRectangleDonut)
		{
			return null;
		}

		private IShapeBase MakeShape(ISymbolRoundedRectangleThermal roundedRectangleThermal)
		{
			return null;
		}

		private IShapeBase MakeShape(ISymbolRoundedRectangleThermalOpenCorners roundedRectangleThermalOpenCorners)
		{
			return null;
		}

		private IShapeBase MakeShape(ISymbolRoundedSquareThermal roundedSquareThermal)
		{
			return null;
		}

		private IShapeBase MakeShape(ISymbolRoundedSquareThermalOpenCorners roundedSquareThermalOpenCorners)
		{
			return null;
		}

		private IShapeBase MakeShape(ISymbolRoundedSqureDonut roundedSqureDonut)
		{
			return null;
		}

		private IShapeBase MakeShape(ISymbolRoundThermalRounded roundThermalRounded)
		{
			return null;
		}

		private IShapeBase MakeShape(ISymbolRoundThermalSquared roundThermalSquared)
		{
			return null;
		}

		private IShapeBase MakeShape(bool useMM, float pixelResolution,
			bool isMM, double x, double y, ISymbolSquareButterfly squareButterfly)
		{
			double halfDiameter = squareButterfly.Diameter / 2;

			var leftTop = MakeShapeRectangle(useMM, pixelResolution, isMM,
				x - halfDiameter, y - halfDiameter, halfDiameter, halfDiameter);

			var rightBottom = MakeShapeRectangle(useMM, pixelResolution, isMM,
				x, y, halfDiameter, halfDiameter);

			return new ShapePolygon(pixelResolution, true, new ShapeBase[] { leftTop, rightBottom });
		}

		private IShapeBase MakeShape(ISymbolSquareDonut squareDonut)
		{
			return null;
		}

		private IShapeBase MakeShape(ISymbolSquareRoundDonut squareRoundDonut)
		{
			return null;
		}

		private IShapeBase MakeShape(ISymbolSquareRoundThermal squareRoundThermal)
		{
			return null;
		}

		private IShapeBase MakeShape(ISymbolSquareThermal squareThermal)
		{
			return null;
		}

		private IShapeBase MakeShape(ISymbolSquareThermalOpenCorners squareThermalOpenCorners)
		{
			return null;
		}

		private IShapeBase MakeShape(ISymbolTriangle triangle)
		{
			return null;
		}

		private IShapeBase MakeShape(ISymbolVerticalHexagon verticalHexagon)
		{
			return null;
		}

		private IShapeBase MakeShape(ISymbolUser user)
		{
			return null;
		}

		private ShapeEllipse MakeShapeEllipse(bool useMM, float pixelResolution,
			bool isMM, double cx, double cy, double width, double height)
		{
			float sx = (float)cx;
			float sy = (float)cy;
			float fWidth = (float)width;
			float fHeight = (float)height;

			if (useMM is true)
			{
				if (isMM is false)
				{
					sx = (float)cx.ConvertInchToMM();
					sy = (float)cy.ConvertInchToMM();
					fWidth = (float)width.ConvertInchToUM();
					fHeight = (float)height.ConvertInchToUM();
				}
			}
			else
			{
				if (isMM is true)
				{
					sx = (float)cx.ConvertMMToInch();
					sy = (float)cy.ConvertMMToInch();
					fWidth = (float)width.ConvertUMToInch();
					fHeight = (float)height.ConvertUMToInch();
				}
			}

			// Gdi에 그릴때는 LT부터 width, height 만큼 그림
			// ODB에서 LT 좌표는 (sx - fWidth / 2), (sy + fHeight / 2) 
			// 하지만 Gdi는 y좌표가 반대이므로 -1곱한다
			return new ShapeEllipse(pixelResolution, (sx - fWidth / 2), -(sy + fHeight / 2), fWidth, fHeight);
		}

		private ShapeRectangle MakeShapeRectangle(bool useMM, float pixelResolution,
			bool isMM, double cx, double cy, double width, double height)
		{
			float sx = (float)cx;
			float sy = (float)cy;
			float fWidth = (float)width;
			float fHeight = (float)height;

			if (useMM is true)
			{
				if (isMM is false)
				{
					sx = (float)cx.ConvertInchToMM();
					sy = (float)cy.ConvertInchToMM();
					fWidth = (float)width.ConvertInchToUM();
					fHeight = (float)height.ConvertInchToUM();
				}
			}
			else
			{
				if (isMM is true)
				{
					sx = (float)cx.ConvertMMToInch();
					sy = (float)cy.ConvertMMToInch();
					fWidth = (float)width.ConvertUMToInch();
					fHeight = (float)height.ConvertUMToInch();
				}
			}

			// Gdi에 그릴때는 LT부터 width, height 만큼 그림
			// ODB에서 LT 좌표는 (sx - fWidth / 2), (sy + fHeight / 2) 
			// 하지만 Gdi는 y좌표가 반대이므로 -1곱한다
			return new ShapeRectangle(pixelResolution, (sx - fWidth / 2), -(sy + fHeight / 2), fWidth, fHeight);
		}
	}
}
