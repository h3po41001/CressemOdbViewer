using ImageControl.Shape.Interface;

namespace CressemCADViewer.Model.Shape
{
	internal class ShapeBase : IShapeBase
	{
		protected ShapeBase() { }

		protected ShapeBase(float pixelResolution)
		{
			PixelResolution = pixelResolution;
		}

		public float PixelResolution { get; private set; }
	}
}
