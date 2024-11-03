using System.Collections.Generic;
using CressemDataToGraphics.Model.Graphics.Shape;
using CressemExtractLibrary.Data.Interface.Features;
using ImageControl.Shape.Interface;

namespace CressemDataToGraphics.Factory
{
	internal class GdiPlusFactory : GraphicsFactory
	{
		public GdiPlusFactory()
		{
		}

		public override IGraphicsShape CreateArc(bool useMM,
			float pixelResolution, bool isMM,
			double datumX, double datumY,
			double cx, double cy,
			int orient, bool isFlipHorizontal,
			double sx, double sy,
			double ex, double ey,
			double arcCx, double arcCy,
			bool isClockWise, double width)
		{
			return ShapeGdiArc.Create(useMM,
				pixelResolution, isMM,
				datumX, datumY, cx, cy,
				orient, isFlipHorizontal,
				sx, sy, ex, ey, arcCx, arcCy,
				isClockWise, width);
		}

		public override IGraphicsShape CreateLine(bool useMM,
			float pixelResolution, bool isMM,
			double datumX, double datumY,
			double cx, double cy,
			int orient, bool isFlipHorizontal,
			double sx, double sy,
			double ex, double ey, double width)
		{
			return ShapeGdiLine.Create(useMM,
				pixelResolution, isMM,
				datumX, datumY,
				cx, cy,
				orient, isFlipHorizontal,
				sx, sy, ex, ey, width);
		}

		public override IGraphicsShape CreateSurface(bool useMM,
			float pixelResolution, bool isMM,
			double datumX, double datumY, 
			double cx, double cy,
			int orient, bool isFlipHorizontal,
			bool isPositive, IEnumerable<IFeaturePolygon> featurePolygons)
		{
			return ShapeGdiSurface.Create(useMM,
				pixelResolution, isMM,
				datumX, datumY,
				cx, cy,
				orient, isFlipHorizontal,
				isPositive, featurePolygons);
		}

		public override IGraphicsShape CreateSurface(bool isPositive,
			IEnumerable<IGraphicsShape> shapes)
		{
			return new ShapeGdiSurface(isPositive, shapes);
		}

		public override IGraphicsShape CreateEllipse(bool useMM,
			float pixelResolution, bool isMM,
			double datumX, double datumY, 
			double cx, double cy,
			int orient, bool isFlipHorizontal,
			double width, double height)
		{
			throw new System.NotImplementedException();
		}

		public override IGraphicsShape CreateRectangle(bool useMM,
			float pixelResolution, bool isMM,
			double datumX, double datumY, 
			double cx, double cy,
			int orient, bool isFlipHorizontal,
			double width, double height)
		{
			throw new System.NotImplementedException();
		}

		public override IGraphicsShape CreatePolygon(bool isFill,
			IEnumerable<IGraphicsShape> shapes)
		{
			throw new System.NotImplementedException();
		}
	}
}
