using CressemExtractLibrary.Data.Interface.Features;
using ImageControl.Model;
using ImageControl.Shape.Interface;

namespace CressemDataToGraphics.Factory
{
	internal class DataToGraphicsFactory
	{
		private static DataToGraphicsFactory _instance;
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

		public IGraphicsList DataToGraphics(bool useMM, 
			float pixelResolution,
			double globalDatumX, double globalDatumY,
			double localDatumX, double localDatumY,
			double cx, double cy,
			int orient, bool isFlipHorizontal, IFeatureBase feature)
		{
			if (feature is null)
			{
				return null;
			}

			return _graphicsFactory.CreateFeatureToShape(useMM,
				pixelResolution,
				globalDatumX, globalDatumY,
				localDatumX, localDatumY, cx, cy,
				orient, isFlipHorizontal, feature);
		}
	}
}
