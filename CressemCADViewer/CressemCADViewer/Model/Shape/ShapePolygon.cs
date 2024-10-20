using System;
using System.Collections.Generic;
using ImageControl.Shape.Interface;

namespace CressemCADViewer.Model.Shape
{
	internal class ShapePolygon : ShapeBase, IShapePolygon
	{
		public ShapePolygon() : base()
		{
		}

		public IEnumerable<IShapeBase> Shapes { get; set; }
	}
}
