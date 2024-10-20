using ImageControl.Shape.Interface;

namespace CressemCADViewer.Model.Shape
{
	internal class ShapeBase : IShapeBase
	{
		protected ShapeBase()
		{
		}

		public float PixelResolution { get; set; }

		public bool IsFill { get; set; }
	}
}
