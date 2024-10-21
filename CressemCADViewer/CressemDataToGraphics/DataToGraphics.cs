using CressemDataToGraphics.Factory;
using CressemDataToGraphics.Model;
using CressemExtractLibrary.Data.Interface.Features;
using ImageControl.Shape.Interface;

namespace CressemDataToGraphics
{
	public class DataToGraphics
	{
		private DataToGraphics() { }

		public DataToGraphics(float pixelResolution,
			GraphicsType graphics)
		{
			PixelResolution = pixelResolution;
			GraphicsType = graphics;
		}

		public float PixelResolution { get; private set; }

		public GraphicsType GraphicsType { get; private set; }

		public IShapeBase GetShape(bool useMM, IFeatureBase feature)
		{
			if (feature is null)
			{
				return null;
			}

			if (GraphicsType is GraphicsType.GdiPlus)
			{
				return DataToGraphicsFactory.Instance.DataToGdiPlus(useMM,
					PixelResolution, feature);
			}
			else if (GraphicsType is GraphicsType.OpenGl)
			{
				return DataToGraphicsFactory.Instance.DataToGdiPlus(useMM,
					PixelResolution, feature);
			}
			else
			{
				return null;
			}
		}
	}
}
