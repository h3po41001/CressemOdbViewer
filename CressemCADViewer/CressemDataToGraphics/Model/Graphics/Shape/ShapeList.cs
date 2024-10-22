using System.Collections.Generic;
using ImageControl.Shape.Interface;

namespace CressemDataToGraphics.Model.Graphics.Shape
{
	internal class ShapeList : IShapeList
	{
		private List<IShapeBase> _shapes;

		public ShapeList()
		{
			_shapes = new List<IShapeBase>();
		}

		public IEnumerable<IShapeBase> Shapes { get => _shapes; }

		public void AddShape(IShapeBase shape)
		{
			_shapes.Add(shape);
		}

		public void AddShape(IShapeList shapes)
		{
			_shapes.AddRange(shapes.Shapes);
		}
	}
}
