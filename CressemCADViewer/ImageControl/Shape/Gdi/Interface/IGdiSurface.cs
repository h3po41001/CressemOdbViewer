using System.Collections.Generic;
using ImageControl.Shape.Interface;

namespace ImageControl.Shape.Gdi.Interface
{
	public interface IGdiSurface : IGraphicsShape
	{
		bool IsPositive { get; }

		IEnumerable<IGraphicsShape> Polygons { get; }
	}
}
