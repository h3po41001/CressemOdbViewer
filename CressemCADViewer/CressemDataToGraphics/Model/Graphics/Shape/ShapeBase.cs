using ImageControl.Shape.Interface;

namespace CressemDataToGraphics.Model.Graphics.Shape
{
	internal abstract class ShapeBase : IShapeBase
	{
		protected ShapeBase() { }

		protected ShapeBase(float pixelResolution)
		{
			PixelResolution = pixelResolution;
		}

		public float PixelResolution { get; private set; }
	}
}
