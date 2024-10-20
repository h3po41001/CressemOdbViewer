using System.Collections.Generic;

namespace ImageControl.Shape.Interface
{
	public interface IShapeSurface : IShapeBase
	{
		IEnumerable<IShapePolygon> Polygons { get; set; }
	}
}
