using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageControl.Shape.Interface;

namespace CressemCADViewer.Model.Shape
{
	internal class ShapeSurface : ShapeBase, IShapeSurface
	{
		public ShapeSurface() : base()
		{
		}

		public IEnumerable<IShapePolygon> Polygons { get; set; }
	}
}
