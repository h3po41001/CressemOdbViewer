using System.Collections.Generic;

namespace ImageControl.Shape.Gdi.Interface
{
	public interface IGdiSurface : IGdiShape
	{
		bool IsPositive { get; }

		IEnumerable<IGdiPolygon> Polygons { get; }
	}
}
