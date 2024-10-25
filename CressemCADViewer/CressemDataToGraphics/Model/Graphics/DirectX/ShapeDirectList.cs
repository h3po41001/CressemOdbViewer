using System.Collections.Generic;
using ImageControl.Shape.DirectX.Interface;

namespace CressemDataToGraphics.Model.Graphics.DirectX
{
	internal class ShapeDirectList : IDirectList
	{
		private ShapeDirectList() { }

		public ShapeDirectList(IEnumerable<IDirectShape> shapes)
		{
			Shapes = shapes;
		}

		public IEnumerable<IDirectShape> Shapes { get; private set; }
	}
}
