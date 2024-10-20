using ImageControl.Shape.Interface;

namespace CressemCADViewer.Model.Shape
{
	internal class ShapeArc : ShapeBase, IShapeArc
	{
		public ShapeArc() : base()
		{
		}

		public float Sx { get; set; }

		public float Sy { get; set; }

		public float Ex { get; set; }

		public float Ey { get; set; }

		public float Cx { get; set; }

		public float Cy { get; set; }

		public bool IsClockWise { get; set; }
	}
}
