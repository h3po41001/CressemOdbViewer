using System.Collections.Generic;
using ImageControl.Shape.Interface;

namespace CressemDataToGraphics.Model.Graphics.Shape
{
	internal class ShapeList : IGdiList
	{
		private readonly List<IGdiBase> _shapes;

		//private ShapeList() { }

		public ShapeList()
		{
			_shapes = new List<IGdiBase>();
		}

		public IEnumerable<IGdiBase> Shapes { get => _shapes; }

		public void AddShape(IEnumerable<IGdiBase> shapes)
		{
			if (shapes != null)
			{
				_shapes.AddRange(shapes);
			}
		}
	}
}
