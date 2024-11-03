using System.Collections.Generic;
using CressemDataToGraphics.Model.Graphics.DirectX;
using CressemExtractLibrary.Data.Interface.Features;
using ImageControl.Shape.Interface;

namespace CressemDataToGraphics.Factory
{
	internal class DirectXFactory : GraphicsFactory
	{
		public DirectXFactory() 
		{
		}

		public override IGraphicsShape CreateArc(bool useMM,
			float pixelResolution, bool isMM, 
			double globalDatumX, double globalDatumY,
			double localDatumX, double localDatumY,
			double cx, double cy, 
			int orient, bool isFlipHorizontal, 
			double sx, double sy, 
			double ex, double ey, 
			double arcCx, double arcCy, 
			bool isClockWise, double width)
		{
			return ShapeDirectArc.Create(useMM,
				pixelResolution, isMM,
				globalDatumX, globalDatumY,
				localDatumX, localDatumY, 
				cx, cy, 
				orient, isFlipHorizontal,
				sx, sy, ex, ey, arcCx, arcCy, isClockWise, width);
		}

		public override IGraphicsShape CreateLine(bool useMM, 
			float pixelResolution, bool isMM,
			double globalDatumX, double globalDatumY,
			double localDatumX, double localDatumY, double cx, double cy, 
			int orient, bool isFlipHorizontal, 
			double sx, double sy, 
			double ex, double ey, double width)
		{
			return ShapeDirectLine.Create(useMM,
				pixelResolution, isMM,
				globalDatumX, globalDatumY,
				localDatumX, localDatumY, 
				cx, cy,
				orient, isFlipHorizontal,
				sx, sy, ex, ey, width);
		}

		public override IGraphicsShape CreateSurface(bool useMM, 
			float pixelResolution, bool isMM,
			double globalDatumX, double globalDatumY,
			double localDatumX, double localDatumY, double cx, double cy,
			int orient, bool isFlipHorizontal, 
			bool isPositive, IEnumerable<IFeaturePolygon> featurePolygons)
		{
			return ShapeDirectSurface.Create(useMM,			
				pixelResolution, isMM,
				globalDatumX, globalDatumY,
				localDatumX, localDatumY, 
				cx, cy,
				orient, isFlipHorizontal,
				isPositive, featurePolygons);
		}

		public override IGraphicsShape CreateEllipse(bool useMM, 
			float pixelResolution, bool isMM,
			double globalDatumX, double globalDatumY,
			double localDatumX, double localDatumY, double cx, double cy, 
			int orient, bool isFlipHorizontal, 
			double width, double height)
		{
			return ShapeDirectEllipse.Create(useMM,
				pixelResolution, isMM,
				globalDatumX, globalDatumY,
				localDatumX, localDatumY, 
				cx, cy, orient, isFlipHorizontal,
				width, height);
		}

		public override IGraphicsShape CreateRectangle(bool useMM, 
			float pixelResolution, bool isMM,
			double globalDatumX, double globalDatumY,
			double localDatumX, double localDatumY, double cx, double cy, 
			int orient, bool isFlipHorizontal, 
			double width, double height)
		{
			return ShapeDirectRectangle.Create(useMM,
				pixelResolution, isMM,
				globalDatumX, globalDatumY,
				localDatumX, localDatumY, 
				cx, cy, orient, isFlipHorizontal,
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
	}
}
