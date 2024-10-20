using ImageControl.Shape.Interface;

namespace CressemCADViewer.Model.Shape
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
	}
}
