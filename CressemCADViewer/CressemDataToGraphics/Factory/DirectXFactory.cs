using System.Collections.Generic;
using System.Drawing;
using CressemDataToGraphics.Model.Graphics;
using CressemDataToGraphics.Model.Graphics.DirectX;
using CressemExtractLibrary.Data.Interface.Features;
using CressemExtractLibrary.Data.Interface.Font;
using ImageControl.Extension;
using ImageControl.Shape.Interface;

namespace CressemDataToGraphics.Factory
{
	internal class DirectXFactory : GraphicsFactory
	{
		public DirectXFactory()
		{
		}

		public override IGraphicsShape CreateArc(bool useMM, float pixelResolution,
			int globalOrient, bool isGlobalFlipHorizontal,
			bool isMM, double datumX, double datumY,
			double anchorX, double anchorY,
			double cx, double cy,
			int orient, bool isFlipHorizontal,
			double sx, double sy,
			double ex, double ey,
			double arcCx, double arcCy,
			bool isClockWise, double width)
		{
			return ShapeDirectArc.Create(useMM, pixelResolution,
				globalOrient, isGlobalFlipHorizontal,
				isMM, datumX, datumY,
				anchorX, anchorY,
				cx, cy,
				orient, isFlipHorizontal,
				sx, sy, ex, ey, arcCx, arcCy, isClockWise, width);
		}

		public override IGraphicsShape CreateLine(bool useMM, float pixelResolution,
			int globalOrient, bool isGlobalFlipHorizontal,
			bool isMM, double datumX, double datumY,
			double anchorX, double anchorY,
			double cx, double cy,
			int orient, bool isFlipHorizontal,
			double sx, double sy,
			double ex, double ey, double width)
		{
			return ShapeDirectLine.Create(useMM, pixelResolution,
				globalOrient, isGlobalFlipHorizontal,
				isMM, datumX, datumY,
				anchorX, anchorY,
				cx, cy,
				orient, isFlipHorizontal,
				sx, sy, ex, ey, width);
		}

		public override IGraphicsShape CreateSurface(bool useMM, float pixelResolution,
			int globalOrient, bool isGlobalFlipHorizontal,
			bool isMM, double datumX, double datumY,
			double cx, double cy,
			int orient, bool isFlipHorizontal,
			bool isPositive, IEnumerable<IFeaturePolygon> featurePolygons)
		{
			return ShapeDirectSurface.Create(useMM, pixelResolution,
				globalOrient, isGlobalFlipHorizontal,
				isMM, datumX, datumY,
				cx, cy,
				orient, isFlipHorizontal,
				isPositive, featurePolygons);
		}

		public override IGraphicsShape CreateEllipse(bool useMM, float pixelResolution,
			int globalOrient, bool isGlobalFlipHorizontal, bool isMM,
			double datumX, double datumY,
			double anchorX, double anchorY,
			double cx, double cy,
			int orient, bool isFlipHorizontal,
			double width, double height)
		{
			return ShapeDirectEllipse.Create(useMM, pixelResolution,
				globalOrient, isGlobalFlipHorizontal,
				isMM, datumX, datumY,
				anchorX, anchorY,
				cx, cy,
				orient, isFlipHorizontal,
				width, height);
		}

		public override IGraphicsShape CreateRectangle(bool useMM, float pixelResolution,
			int globalOrient, bool isGlobalFlipHorizontal,
			bool isMM, double datumX, double datumY,
			double anchorX, double anchorY,
			double cx, double cy,
			int orient, bool isFlipHorizontal,
			double width, double height)
		{
			return ShapeDirectRectangle.Create(useMM, pixelResolution,
				globalOrient, isGlobalFlipHorizontal,
				isMM, datumX, datumY,
				anchorX, anchorY,
				cx, cy,
				orient, isFlipHorizontal,
				width, height);
		}

		public override IGraphicsShape CreateSurface(bool isPositive,
			IEnumerable<IGraphicsShape> shapes)
		{
			return new ShapeDirectSurface(isPositive, shapes);
		}

		public override IGraphicsShape CreatePolygon(bool isFill,
			IEnumerable<IGraphicsShape> shapes)
		{
			return new ShapeDirectPolygon(isFill, shapes);
		}

		public override IGraphicsList CreateFont(bool useMM, float pixelesolution,
			int globalOrient, bool isGlobalFlipHorizontal,
			bool isMM, double datumX, double datumY,
			double anchorX, double anchorY,
			double cx, double cy,
			int orient, bool isFlipHorizontal,
			string target,
			double xSize, double ySize, double widthFactor,
			IFont font)
		{
			char[] chars = target.ToCharArray();
			if (chars.Length < 1)
			{
				return null;
			}

			List<IGraphicsShape> charShapes = new List<IGraphicsShape>();
			//12 mil 사각형 안에서 그려야함
			//12 mil 안에서 그리고 확장시켜서 그려야함
			// xSize : font.XSize = x : 12
			int idx = 0;

			foreach (var each in chars)
			{
				foreach (var attr in font.FontAttrs)
				{
					if (each.ToString().Equals(attr.Character, System.StringComparison.Ordinal) is true)
					{
						foreach (var fontLine in attr.FontLines)
						{
							double fontWidth = fontLine.Width * 2.5 * widthFactor;
							double fontToTextX = (xSize - fontLine.Width * widthFactor * 2) / font.XSize;
							double fontToTextY = (ySize - fontLine.Width * widthFactor * 2) / font.YSize;
							double offset = (font.Offset * fontToTextX + xSize) * idx;
							double lineWidth = fontLine.Width * widthFactor * 1000;

							// 끝이 사각
							if (fontLine.Shape.ToUpper().Equals("S") is true)
							{
								ShapeDirectRectangle startRect = ShapeDirectRectangle.Create(
									useMM, pixelesolution,
									globalOrient, isGlobalFlipHorizontal,
									isMM, datumX + cx, datumY + cy,
									cx, cy,
									fontLine.SX * fontToTextX + offset + fontWidth,
									fontLine.SY * fontToTextY + fontWidth,
									orient, isFlipHorizontal,
									lineWidth, lineWidth);

								ShapeDirectRectangle endRect = ShapeDirectRectangle.Create(
									useMM, pixelesolution,
									globalOrient, isGlobalFlipHorizontal,
									isMM, datumX + cx, datumY + cy,
									cx, cy,
									fontLine.EX * fontToTextX + offset + fontWidth,
									fontLine.EY * fontToTextY + fontWidth,
									orient, isFlipHorizontal,
									lineWidth, lineWidth);

								charShapes.Add(startRect);
								charShapes.Add(endRect);
							}
							// 끝이 원
							else if (fontLine.Shape.ToUpper().Equals("R") is true)
							{
								ShapeDirectEllipse startEllipse = ShapeDirectEllipse.Create(
									useMM, pixelesolution,
									globalOrient, isGlobalFlipHorizontal,
									isMM, datumX + cx, datumY + cy,
									cx, cy,
									fontLine.SX * fontToTextX + offset + fontWidth,
									fontLine.SY * fontToTextY + fontWidth, 
									orient, isFlipHorizontal,
									lineWidth, lineWidth);

								ShapeDirectEllipse endEllipse = ShapeDirectEllipse.Create(
									useMM, pixelesolution,
									globalOrient, isGlobalFlipHorizontal,
									isMM, datumX + cx, datumY + cy,
									cx, cy,
									fontLine.EX * fontToTextX + offset + fontWidth, 
									fontLine.EY * fontToTextY + fontWidth,
									orient, isFlipHorizontal,
									lineWidth, lineWidth);

								charShapes.Add(startEllipse);
								charShapes.Add(endEllipse);
							}

							ShapeDirectLine line = ShapeDirectLine.Create(useMM, pixelesolution,
								globalOrient, isGlobalFlipHorizontal,
								isMM, datumX + cx, datumY + cy,
								cx, cy,
								cx, cy,
								orient, isFlipHorizontal,
								fontLine.SX * fontToTextX + offset + fontWidth, 
								fontLine.SY * fontToTextY + fontWidth,
								fontLine.EX * fontToTextX + offset + fontWidth, 
								fontLine.EY * fontToTextY + fontWidth,
								lineWidth);

							charShapes.Add(line);
						}
					}
				}

				idx++;
			}

			return new ShapeGraphicsList(true, charShapes);
		}
	}
}
