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

		public void AddShape(object shape)
		{
			if (shape != null)
			{
				if (shape is IShapeBase shapeBase)
				{
					_shapes.Add(shapeBase);
				}
                else if (shape is IShapeList shapeList)
				{
					_shapes.AddRange(shapeList.Shapes);
				}
			}
		}
	}
}
