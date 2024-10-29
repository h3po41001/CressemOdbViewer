using System.Collections.Generic;
using ImageControl.Shape.Gdi.Interface;

namespace CressemDataToGraphics.Model.Graphics.Shape
{
	internal class ShapeGdiList : IGdiList
	{
		private ShapeGdiList() { }

		public ShapeGdiList(bool isPositive, 
			IEnumerable<IGdiShape> shapes)
		{
			IsPositive = isPositive;
			Shapes = new List<IGdiShape>(shapes);
		}

		public bool IsPositive { get; private set; }

		public IEnumerable<IGdiShape> Shapes { get; private set; }
	}
}
