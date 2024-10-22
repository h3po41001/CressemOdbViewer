using System;
using CressemDataToGraphics.Converter;
using CressemDataToGraphics.Model.Graphics.Shape;
using CressemExtractLibrary.Data.Interface.Features;
using ImageControl.Shape.Interface;

namespace CressemDataToGraphics.Factory
{
	internal class DataToGraphicsFactory
	{
		private static DataToGraphicsFactory _instance;

		private readonly GdiPlusFactory _gdiPlusFactory;
		private readonly OpenGlFactory _openGlFactory;

		private DataToGraphicsFactory()
		{
			_gdiPlusFactory = new GdiPlusFactory();
			_openGlFactory = new OpenGlFactory();
		}

		public static DataToGraphicsFactory Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new DataToGraphicsFactory();
				}

				return _instance;
			}
		}

		public IShapeBase[] DataToGdiPlus(bool useMM, float pixelResolution,
			double xDatum, double yDatum, IFeatureBase feature)
		{
			if (feature is null)
			{
				return null;
			}

			var featureShape = _gdiPlusFactory.CreateFeatureToShape(useMM, pixelResolution, feature);
			var symbolShape = _gdiPlusFactory.CreateSymbolToShape(useMM, pixelResolution,
				feature.IsMM, feature.X, feature.Y, feature.FeatureSymbol);
			
			return new IShapeBase[] { featureShape, symbolShape };
		}

		public IShapeBase DataToOpenGl(bool useMM, float pixelResolution,
			double xDatum, double yDatum, IFeatureBase feature)
		{
			throw new NotImplementedException("Not Implemented Shape [Open Gl]");
		}
	}
}
