using System.Collections.Generic;

namespace ImageControl.Shape.DirectX.Interface
{
	public interface IDirectSurface : IDirectShape
	{
		bool IsPositive { get; }

		IEnumerable<IDirectPolygon> Polygons { get; }
	}
}
