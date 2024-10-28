using System.Collections.Generic;
using System.Linq;
using ImageControl.Shape.Gdi.Interface;
using ImageControl.Shape.Interface;

namespace CressemDataToGraphics.Model.Graphics.Shape
{
	internal class ShapeGdiList : IGdiList
	{
		private ShapeGdiList() { }

		public ShapeGdiList(IEnumerable<IGdiShape> shapes)
		{
			Shapes = new List<IGdiShape>(shapes);
		}

		public IEnumerable<IGdiShape> Shapes { get; private set; }
	}
}
