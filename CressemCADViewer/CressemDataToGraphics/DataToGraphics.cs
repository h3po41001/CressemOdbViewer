using CressemDataToGraphics.Factory;
using CressemDataToGraphics.Model;
using CressemExtractLibrary.Data.Interface.Features;
using ImageControl.Model;
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
			DataToGraphicsFactory.Instance.Initialize(graphics);
		}

		public float PixelResolution { get; private set; }

		public IGraphicsList GetShapes(bool useMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis, IFeatureBase feature)
		{
			if (feature is null)
			{
				return null;
			}

			return DataToGraphicsFactory.Instance.DataToGraphics(useMM,
				PixelResolution, xDatum, yDatum, cx, cy, orient, isMirrorXAxis, feature);
		}
	}
}
