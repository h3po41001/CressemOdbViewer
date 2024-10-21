using ImageControl.Shape.Interface;

namespace CressemDataToGraphics.Model.Graphics.Shape
{
	internal class ShapeRectangle : ShapeBase, IShapeRectangle
	{
		public ShapeRectangle() : base()
		{
		}

		public float X { get; set; }

		public float Y { get; set; }

		public float Width { get; set; }

		public float Height { get; set; }

		public static IShapeRectangle CreateGdiPlus(float pixelResolution)
		{
			throw new System.NotImplementedException();
		}

		public static IShapeRectangle CreateOpenGl()
		{
			throw new System.NotImplementedException();
		}
	}
}
