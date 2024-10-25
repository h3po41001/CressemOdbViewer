using System;
using CressemExtractLibrary.Data.Interface.Features;
using ImageControl.Shape.DirectX.Interface;

namespace CressemDataToGraphics.Factory
{
	internal class DirectXFactory
	{
		public DirectXFactory()
		{
		}

		public IDirectShape CreateFeatureToShape(bool useMM,
			float pixelResolution,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis, IFeatureBase feature)
		{
			throw new NotImplementedException();
		}
	}
}
