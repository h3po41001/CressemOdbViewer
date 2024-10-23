using System.Collections.Generic;
using ImageControl.Shape.Interface;

namespace CressemDataToGraphics.Model.Graphics.Shape
{
	internal class ShapeList : IShapeList
	{
		private List<IShapeBase> _shapes;

		private ShapeList()		{					}

		public ShapeList(double xDatum, double yDatum, int globalOrient)
		{
			Xdatum = (float)xDatum;
			Ydatum = (float)yDatum;
			Orient = globalOrient;
			_shapes = new List<IShapeBase>();
		}

		public float Xdatum { get; private set; }

		public float Ydatum { get; private set; }

		public int Orient { get; private set; }

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
