using ImageControl.Shape.Interface;

namespace CressemCADViewer.Model.Shape
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
	}
}
