using System.Collections.Generic;
using System.Linq;
using ImageControl.Shape.Gdi.Interface;
using ImageControl.Shape.Interface;

namespace CressemDataToGraphics.Model.Graphics.Shape
{
	internal class ShapeGdiList : IGdiList
	{
		private readonly List<IGdiShape> _shapes;

		//private ShapeList() { }

		public ShapeGdiList()
		{
			_shapes = new List<IGdiShape>();
		}

		public IEnumerable<IGdiShape> Shapes { get => _shapes; }

		public void AddShape(IEnumerable<IGdiShape> shapes)
		{
			if (shapes != null)
			{
				_shapes.AddRange(shapes);
			}
		}
	}
}
