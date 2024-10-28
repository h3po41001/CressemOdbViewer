using System.Collections.Generic;
using CressemDataToGraphics.Model.Graphics.Shape;
using CressemExtractLibrary.Data.Interface.Features;
using CressemExtractLibrary.Data.Interface.Symbol;
using ImageControl.Shape.Gdi.Interface;

namespace CressemDataToGraphics.Factory
{
	internal class GdiPlusFactory
	{
		public GdiPlusFactory()
		{
		}

		public IGdiList CreateFeatureToShape(bool useMM,
			float pixelResolution,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis, IFeatureBase feature)
		{
			if (feature is null)
			{
				return null;
			}

			if (feature is IFeaturePolygon)
			{
				return null;
			}

			return MakeFeatureShape(useMM, pixelResolution, feature.IsMM,
				xDatum, yDatum, cx, cy,
				orient, isMirrorXAxis, (dynamic)feature);
		}

		private IGdiList MakeFeatureShape(bool useMM,
			float pixelResolution, bool isMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis, IFeatureArc arc)
		{
			List<ShapeGdiBase> shapes = new List<ShapeGdiBase>();

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

				shapes.Add(ShapeGdiArc.Create(useMM,
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
				shapes.Add(ShapeGdiArc.Create(useMM,
					pixelResolution, isMM,
					xDatum, yDatum, cx, cy,
					orient, isMirrorXAxis,
					arc.X, arc.Y, arc.Ex, arc.Ey, arc.Cx, arc.Cy,
					arc.IsClockWise, 0));
			}

			return new ShapeGdiList(shapes);

		}

		private IGdiList MakeFeatureShape(bool useMM, float pixelResolution,
			double xDatum, double yDatum, IFeatureBarcode barcode)
		{
			throw new System.NotImplementedException("바코드 아직 미구현");
		}

		private IGdiList MakeFeatureShape(bool useMM,
			float pixelResolution, bool isMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis, IFeatureLine line)
		{
			List<ShapeGdiBase> shapes = new List<ShapeGdiBase>();

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

				shapes.Add(ShapeGdiLine.Create(useMM,
					pixelResolution, isMM,
					xDatum, yDatum, cx, cy,
					orient, isMirrorXAxis,
					line.X, line.Y, line.Ex, line.Ey, regularShape.Diameter));

				shapes.Add(startSymbol);
				shapes.Add(endSymbol);
			}
			else
			{
				shapes.Add(ShapeGdiLine.Create(useMM,
					pixelResolution, isMM,
					xDatum, yDatum, cx, cy,
					orient, isMirrorXAxis,
					line.X, line.Y, line.Ex, line.Ey, 0));
			}

			return new ShapeGdiList(shapes);
		}

		private IGdiList MakeFeatureShape(bool useMM,
			float pixelResolution, bool isMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis, IFeaturePad pad)
		{
			List<ShapeGdiBase> shapes = new List<ShapeGdiBase>();

			if (pad.FeatureSymbol != null)
			{
				shapes.Add(MakeSymbolShape(useMM,
					pixelResolution, isMM,
					xDatum + cx, yDatum + cy,
					pad.X, pad.Y,
					(pad.Orient + orient) % 360,
					isMirrorXAxis != pad.IsMirrorXAxis,
					pad.FeatureSymbol));
			}

			return new ShapeGdiList(shapes);
		}

		private IGdiList MakeFeatureShape(bool useMM,
			float pixelResolution, bool isMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis, IFeatureText text)
		{
			throw new System.NotImplementedException("텍스트 아직 미구현");
		}

		private IGdiList MakeFeatureShape(bool useMM,
			float pixelResolution, bool isMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis,
			IFeatureSurface surface)
		{
			List<ShapeGdiBase> shapes = new List<ShapeGdiBase>();

			bool isPositive = surface.Polarity.Equals("P") is true;

			shapes.Add(ShapeGdiSurface.Create(useMM,
				pixelResolution, isMM,
				xDatum, yDatum, cx, cy,
				orient, isMirrorXAxis,
				isPositive, surface.Polygons));

			return new ShapeGdiList(shapes);
		}

		private ShapeGdiBase MakeSymbolShape(bool useMM,
			float pixelResolution, bool isMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis,
			ISymbolBase symbol)
		{
			if (symbol is ISymbolRound round)
			{
				return ShapeGdiEllipse.Create(useMM,
					pixelResolution, isMM,
					xDatum, yDatum, cx, cy,
					orient, isMirrorXAxis,
					round.Diameter, round.Diameter);
			}
			else if (symbol is ISymbolSquare square)
			{
				return ShapeGdiRectangle.Create(useMM,
					pixelResolution, isMM,
					xDatum, yDatum, cx, cy,
					orient, isMirrorXAxis,
					square.Diameter, square.Diameter);
			}
			else if (symbol is ISymbolRectangle rectangle)
			{
				return ShapeGdiRectangle.Create(useMM,
					pixelResolution, isMM,
					xDatum, yDatum, cx, cy,
					orient, isMirrorXAxis,
					rectangle.Width, rectangle.Height);
			}
			else if (symbol is ISymbolEllipse ellipse)
			{
				return ShapeGdiEllipse.Create(useMM,
					pixelResolution, isMM,
					xDatum, yDatum, cx, cy,
					orient, isMirrorXAxis,
					ellipse.Width, ellipse.Height);
			}
			else if (symbol is ISymbolHole hole)
			{

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
			else if (symbol is ISymbolUser user)
			{
				var userSymbolFeature = MakeUser(useMM,
					pixelResolution, isMM,
					xDatum, yDatum, cx, cy,
					orient, isMirrorXAxis,
					user.FeaturesList);
			}

			return null;
		}

		// Rounded Rectangle
		private ShapeGdiSurface MakeRoundedRectangle(bool useMM,
			float pixelResolution, bool isMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis,
			double width, double height, double radius, bool[] isEditedCorner)
		{
			double left = cx - width / 2;
			double right = cx + width / 2;
			double top = cy + height / 2;
			double bottom = cy - height / 2;

			List<ShapeGdiBase> shapeList = new List<ShapeGdiBase>();

			// RT
			if (isEditedCorner[0] is true)
			{
				shapeList.Add(ShapeGdiArc.Create(useMM,
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
				shapeList.Add(ShapeGdiRectangle.Create(useMM,
					pixelResolution, isMM,
					xDatum, yDatum,
					right - radius / 2, top - radius / 2,
					orient, isMirrorXAxis, radius, radius));
			}

			// LT
			if (isEditedCorner[1] is true)
			{
				shapeList.Add(ShapeGdiArc.Create(useMM,
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
				shapeList.Add(ShapeGdiRectangle.Create(useMM,
					pixelResolution, isMM,
					xDatum, yDatum,
					left + radius / 2, top - radius / 2,
					orient, isMirrorXAxis, radius, radius));
			}

			// LB
			if (isEditedCorner[2] is true)
			{
				shapeList.Add(ShapeGdiArc.Create(useMM,
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
				shapeList.Add(ShapeGdiRectangle.Create(useMM,
					pixelResolution, isMM,
					xDatum, yDatum,
					left + radius / 2, bottom + radius / 2,
					orient, isMirrorXAxis, radius, radius));
			}

			// RB
			if (isEditedCorner[3] is true)
			{
				shapeList.Add(ShapeGdiArc.Create(useMM,
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
				shapeList.Add(ShapeGdiRectangle.Create(useMM,
					pixelResolution, isMM,
					xDatum, yDatum,
					right - radius / 2, bottom + radius / 2,
					orient, isMirrorXAxis, radius, radius));
			}

			// Center Rectangle
			shapeList.Add(ShapeGdiRectangle.Create(
				useMM, pixelResolution, isMM,
				xDatum, yDatum, cx, cy,
				orient, isMirrorXAxis,
				width - radius * 2, height));

			shapeList.Add(ShapeGdiRectangle.Create(
				useMM, pixelResolution, isMM,
				xDatum, yDatum, cx, cy,
				orient, isMirrorXAxis,
				width, height - radius * 2));

			var polygon = new ShapeGdiPolygon(true, shapeList);
			return new ShapeGdiSurface(true, new ShapeGdiPolygon[] { polygon });
		}

		private ShapeGdiSurface MakeRoundDonut(bool useMM,
			float pixelResolution, bool isMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis,
			double outerDiameter, double innerDiameter)
		{
			var outerCircle = ShapeGdiEllipse.Create(useMM,
				pixelResolution, isMM,
				xDatum, yDatum, cx, cy,
				orient, isMirrorXAxis,
				outerDiameter, outerDiameter);

			var innerCircle = ShapeGdiEllipse.Create(useMM,
				pixelResolution, isMM,
				xDatum, yDatum, cx, cy,
				orient, isMirrorXAxis,
				innerDiameter, innerDiameter);

			var outerPoly = new ShapeGdiPolygon(true, new ShapeGdiBase[] { outerCircle });
			var innerPoly = new ShapeGdiPolygon(false, new ShapeGdiBase[] { innerCircle });

			return new ShapeGdiSurface(true, new ShapeGdiPolygon[] { outerPoly, innerPoly });
		}

		private IEnumerable<IGdiList> MakeUser(bool useMM,
			float pixelResolution, bool isMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis,
			IEnumerable<IFeatureBase> features)
		{
			List<ShapeGdiList> shapeList = new List<ShapeGdiList>();
			foreach (var feature in features)
			{
				shapeList.Add(MakeFeatureShape(useMM,
					pixelResolution, isMM,
					xDatum, yDatum, cx, cy,
					orient, isMirrorXAxis, (dynamic)feature));
			}

			return shapeList;
		}
	}
}
