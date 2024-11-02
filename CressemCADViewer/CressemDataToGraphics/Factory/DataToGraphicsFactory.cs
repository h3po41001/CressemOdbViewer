using System;
using CressemExtractLibrary.Data.Interface.Features;
using ImageControl.Model;
using ImageControl.Shape.DirectX.Interface;
using ImageControl.Shape.Gdi.Interface;
using ImageControl.Shape.Interface;

namespace CressemDataToGraphics.Factory
{
	internal class DataToGraphicsFactory
	{
		private static DataToGraphicsFactory _instance;

		//private readonly GdiPlusFactory _gdiPlusFactory;
		//private readonly DirectXFactory _directXFactory;
		private GraphicsFactory _graphicsFactory = null;

		private DataToGraphicsFactory()
		{
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

		public void Initialize(GraphicsType graphicsType)
		{
			if (graphicsType is GraphicsType.GdiPlus)
			{
				_graphicsFactory = new GdiPlusFactory();
			}
			else if (graphicsType is GraphicsType.DirectX)
			{
				_graphicsFactory = new DirectXFactory();
			}
			else
			{
				return;
			}
		}

		public IGraphicsList DataToGraphics(bool useMM, float pixelResolution,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis, IFeatureBase feature)
		{
			if (feature is null)
			{
				return null;
			}

			return _graphicsFactory.CreateFeatureToShape(useMM,
				pixelResolution,
				xDatum, yDatum, cx, cy,
				orient, isMirrorXAxis, feature);
		}

		//public IGdiList DataToGdiPlus(bool useMM, float pixelResolution,
		//	double xDatum, double yDatum, double cx, double cy,
		//	int orient, bool isMirrorXAxis, IFeatureBase feature)
		//{
		//	if (feature is null)
		//	{
		//		return null;
		//	}

		//	return _gdiPlusFactory.CreateFeatureToShape(useMM,
		//		pixelResolution,
		//		xDatum, yDatum, cx, cy,
		//		orient, isMirrorXAxis, feature);
		//}

		//public IDirectList DataToDirectX(bool useMM, float pixelResolution,
		//	double xDatum, double yDatum, double cx, double cy,
		//	int orient, bool isMirrorXAxis, IFeatureBase feature)
		//{
		//	if (feature is null)
		//	{
		//		return null;
		//	}

		//	return _directXFactory.CreateFeatureToShape(useMM,
		//		pixelResolution,
		//		xDatum, yDatum, cx, cy,
		//		orient, isMirrorXAxis, feature);
		//}
	}
}
