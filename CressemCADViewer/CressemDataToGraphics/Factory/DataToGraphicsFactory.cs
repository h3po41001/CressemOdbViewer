using System;
using CressemExtractLibrary.Data.Interface.Features;
using ImageControl.Shape.DirectX.Interface;
using ImageControl.Shape.Gdi.Interface;

namespace CressemDataToGraphics.Factory
{
	internal class DataToGraphicsFactory
	{
		private static DataToGraphicsFactory _instance;

		private readonly GdiPlusFactory _gdiPlusFactory;
		private readonly DirectXFactory _directXFactory;

		private DataToGraphicsFactory()
		{
			_gdiPlusFactory = new GdiPlusFactory();
			_directXFactory = new DirectXFactory();
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

		public IGdiList DataToGdiPlus(bool useMM, float pixelResolution,
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

		public IDirectShape DataToDirectX(bool useMM, float pixelResolution,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis, IFeatureBase feature)
		{
			if (feature is null)
			{
				return null;
			}

			return _directXFactory.CreateFeatureToShape(useMM,
				pixelResolution,
				xDatum, yDatum, cx, cy,
				orient, isMirrorXAxis, feature);
		}
	}
}
