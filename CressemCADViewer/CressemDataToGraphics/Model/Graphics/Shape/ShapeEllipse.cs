using ImageControl.Shape.Interface;

namespace CressemDataToGraphics.Model.Graphics.Shape
{
	internal class ShapeEllipse : ShapeBase, IShapeEllipse
	{
		public ShapeEllipse() : base()
		{
		}

		public float X { get; set; }

		public float Y { get; set; }

		public float Width { get; set; }

		public float Height { get; set; }

		public static IShapeEllipse CreateGdiPlus(float pixelResolution)
		{
			throw new System.NotImplementedException();
		}

		public static IShapeEllipse CreateOpenGl(float pixelResolution)
		{
			throw new System.NotImplementedException();
		}
	}
}
