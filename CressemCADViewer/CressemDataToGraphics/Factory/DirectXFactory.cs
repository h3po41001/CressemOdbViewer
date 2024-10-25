using System;
using System.Collections.Generic;
using CressemDataToGraphics.Model.Graphics.DirectX;
using CressemExtractLibrary.Data.Interface.Features;
using CressemExtractLibrary.Data.Interface.Symbol;
using ImageControl.Shape.DirectX.Interface;

namespace CressemDataToGraphics.Factory
{
	internal class DirectXFactory
	{
		public DirectXFactory()
		{
		}

		public IDirectList CreateFeatureToShape(bool useMM,
			float pixelResolution,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis, IFeatureBase feature)
		{
			if (feature is null)
			{
				return null;
			}

			if (feature is IFeatureArc)
			{
				return MakeFeatureShape(useMM, pixelResolution,
					feature.IsMM, xDatum, yDatum, cx, cy,
					orient, isMirrorXAxis, (dynamic)feature);
			}

			return null;
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
				//shapes.Add(ShapeGdiArc.CreateGdiPlus(useMM,
				//	pixelResolution, isMM,
				//	xDatum, yDatum, cx, cy,
				//	orient, isMirrorXAxis,
				//	arc.X, arc.Y, arc.Ex, arc.Ey, arc.Cx, arc.Cy,
				//	arc.IsClockWise, 0));
			}

			return new ShapeDirectList(shapes);
		}

		private IDirectShape MakeSymbolShape(bool useMM,
			float pixelResolution, bool isMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis,
			ISymbolBase symbol)
		{
			//if (symbol is ISymbolRound round)
			//{
			//	return ShapeGdiEllipse.CreateGdiPlus(useMM,
			//		pixelResolution, isMM,
			//		xDatum, yDatum, cx, cy,
			//		orient, isMirrorXAxis,
			//		round.Diameter, round.Diameter);
			//}
			//else if (symbol is ISymbolSquare square)
			//{
			//	return ShapeGdiRectangle.CreateGdiPlus(useMM,
			//		pixelResolution, isMM,
			//		xDatum, yDatum, cx, cy,
			//		orient, isMirrorXAxis,
			//		square.Diameter, square.Diameter);
			//}
			//else if (symbol is ISymbolRectangle rectangle)
			//{
			//	return ShapeGdiRectangle.CreateGdiPlus(useMM,
			//		pixelResolution, isMM,
			//		xDatum, yDatum, cx, cy,
			//		orient, isMirrorXAxis,
			//		rectangle.Width, rectangle.Height);
			//}
			//else if (symbol is ISymbolEllipse ellipse)
			//{
			//	return ShapeGdiEllipse.CreateGdiPlus(useMM,
			//		pixelResolution, isMM,
			//		xDatum, yDatum, cx, cy,
			//		orient, isMirrorXAxis,
			//		ellipse.Width, ellipse.Height);
			//}
			//else if (symbol is ISymbolHole hole)
			//{

			//}
			//else if (symbol is ISymbolRoundedRectangle roundedRectangle)
			//{
			//	return MakeRoundedRectangle(useMM,
			//		pixelResolution, isMM,
			//		xDatum, yDatum, cx, cy,
			//		orient, isMirrorXAxis,
			//		roundedRectangle.Width, roundedRectangle.Height,
			//		roundedRectangle.CornerRadius,
			//		roundedRectangle.IsEditedCorner);
			//}
			//else if (symbol is ISymbolRoundDonut roundDonut)
			//{
			//	return MakeRoundDonut(useMM,
			//		pixelResolution, isMM,
			//		xDatum, yDatum, cx, cy,
			//		orient, isMirrorXAxis,
			//		roundDonut.Diameter, roundDonut.InnerDiameter);
			//}
			//else if (symbol is ISymbolUser user)
			//{
			//	var userSymbolFeature = MakeUser(useMM,
			//		pixelResolution, isMM,
			//		xDatum, yDatum, cx, cy,
			//		orient, isMirrorXAxis,
			//		user.FeaturesList);
			//}

			return null;
		}
	}
}
