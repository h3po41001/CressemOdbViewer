using System.Collections.Generic;
using ImageControl.Shape.Gdi.Interface;

namespace CressemDataToGraphics.Model.Graphics.Shape
{
	internal class ShapeGdiList : IGdiList
	{
		private readonly List<IGdiBase> _shapes;

		//private ShapeList() { }

		public ShapeGdiList()
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
