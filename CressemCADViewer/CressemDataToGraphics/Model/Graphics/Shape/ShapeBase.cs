using ImageControl.Shape.Gdi.Interface;

namespace CressemDataToGraphics.Model.Graphics.Shape
{
	internal abstract class ShapeBase : IGdiBase
	{
		protected ShapeBase() { }

		protected ShapeBase(float pixelResolution)
		{
			PixelResolution = pixelResolution;
		}

		public float PixelResolution { get; private set; }
	}
}
