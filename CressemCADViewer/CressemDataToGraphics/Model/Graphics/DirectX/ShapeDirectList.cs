using System.Collections.Generic;
using ImageControl.Shape.DirectX.Interface;

namespace CressemDataToGraphics.Model.Graphics.DirectX
{
	internal class ShapeDirectList : IDirectList
	{
		private ShapeDirectList() { }

		public ShapeDirectList(bool isPositive, IEnumerable<IDirectShape> shapes)
		{
			IsPositive = isPositive;
			Shapes = new List<IDirectShape>(shapes);
		}

		public bool IsPositive { get; private set; }

		public IEnumerable<IDirectShape> Shapes { get; private set; }
	}
}
