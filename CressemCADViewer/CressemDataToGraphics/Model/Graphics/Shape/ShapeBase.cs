using ImageControl.Shape.Interface;

namespace CressemDataToGraphics.Model.Graphics.Shape
{
	internal class ShapeBase : IShapeBase
	{
		protected ShapeBase() { }

		protected ShapeBase(float pixelResolution,
			float xDatum, float yDatum, int orient)
		{
			PixelResolution = pixelResolution;
			Xdatum = xDatum;
			Ydatum = yDatum;
			Orient = orient;
		}

		public float PixelResolution { get; private set; }

		public float Xdatum { get; private set; }

		public float Ydatum { get; private set; }

		public int Orient { get; set; }
	}
}
