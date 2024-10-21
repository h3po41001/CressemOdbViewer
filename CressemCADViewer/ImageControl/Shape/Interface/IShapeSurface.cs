using System.Collections.Generic;

namespace ImageControl.Shape.Interface
{
	public interface IShapeSurface : IShapeBase
	{
		bool IsPositive { get; }

		IEnumerable<IShapePolygon> Polygons { get; }
	}
}
