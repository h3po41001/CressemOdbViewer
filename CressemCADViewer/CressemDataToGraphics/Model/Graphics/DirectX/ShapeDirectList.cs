using System.Collections.Generic;
using System.Linq;
using ImageControl.Shape.DirectX.Interface;

namespace CressemDataToGraphics.Model.Graphics.DirectX
{
	internal class ShapeDirectList : IDirectList
	{
		private ShapeDirectList() { }

		public ShapeDirectList(IEnumerable<IDirectShape> shapes)
		{
			Shapes = new List<IDirectShape>(shapes.Cast<IDirectShape>());
		}

		public IEnumerable<IDirectShape> Shapes { get; private set; }
	}
}
