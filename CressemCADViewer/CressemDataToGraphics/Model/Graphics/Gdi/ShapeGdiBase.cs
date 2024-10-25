using ImageControl.Shape.Gdi.Interface;

namespace CressemDataToGraphics.Model.Graphics.Shape
{
	internal abstract class ShapeGdiBase : IGdiBase
	{
		protected ShapeGdiBase() { }

		protected ShapeGdiBase(float pixelResolution)
		{
			PixelResolution = pixelResolution;
		}

		public float PixelResolution { get; private set; }
	}
}
