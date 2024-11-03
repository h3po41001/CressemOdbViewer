using System.Collections.Generic;
using ImageControl.Shape.Interface;

namespace CressemDataToGraphics.Model.Graphics
{
	internal class ShapeGraphicsList : IGraphicsList
	{
		private ShapeGraphicsList() { }

		public ShapeGraphicsList(bool isPositive, IEnumerable<IGraphicsShape> shapes)
		{
			IsPositive = isPositive;
			Shapes = new List<IGraphicsShape>(shapes);
		}

		public bool IsPositive { get; private set; }

		public IEnumerable<IGraphicsShape> Shapes { get; private set; }
	}
}
