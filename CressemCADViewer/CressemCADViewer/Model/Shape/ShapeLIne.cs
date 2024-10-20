using ImageControl.Shape.Interface;

namespace CressemCADViewer.Model.Shape
{
	internal class ShapeLine : ShapeBase, IShapeLine
	{
		public ShapeLine() : base()
		{
		}

		public float Sx { get; set; }

		public float Sy { get; set; }

		public float Ex { get; set; }

		public float Ey { get; set; }
	}
}
