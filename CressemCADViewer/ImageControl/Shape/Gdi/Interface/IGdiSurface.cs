using System.Collections.Generic;

namespace ImageControl.Shape.Gdi.Interface
{
	public interface IGdiSurface : IGdiBase
	{
		bool IsPositive { get; }

		IEnumerable<IGdiPolygon> Polygons { get; }
	}
}
