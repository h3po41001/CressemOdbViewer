using System;
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
				if (_instance is null)
				{
					_instance = new DataToGraphicsFactory();
				}

				return _instance;
			}
		}

		public IShapeList DataToGdiPlus(bool useMM, float pixelResolution,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis, IFeatureBase feature)
		{
			if (feature is null)
			{
				return null;
			}

			return _gdiPlusFactory.CreateFeatureToShape(useMM,
				pixelResolution,
				xDatum, yDatum, cx, cy,
				orient, isMirrorXAxis, feature);
		}

		public IShapeBase DataToOpenGl(bool useMM, float pixelResolution,
			double xDatum, double yDatum, IFeatureBase feature)
		{
			throw new NotImplementedException("Not Implemented Shape [Open Gl]");
		}
	}
}
