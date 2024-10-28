using ImageControl.Shape.Gdi.Interface;

namespace CressemDataToGraphics.Model.Graphics.Shape
{
	internal abstract class ShapeGdiBase : IGdiShape
	{
		protected ShapeGdiBase() { }

		protected ShapeGdiBase(float pixelResolution)
		{
			PixelResolution = pixelResolution;
		}

		public float PixelResolution { get; private set; }
	}
}
