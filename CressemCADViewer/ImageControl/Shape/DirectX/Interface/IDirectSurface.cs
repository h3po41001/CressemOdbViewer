using System.Collections.Generic;
using ImageControl.Shape.Interface;

namespace ImageControl.Shape.DirectX.Interface
{
	public interface IDirectSurface : IGraphicsShape
	{
		bool IsPositive { get; }

		IEnumerable<IGraphicsShape> Polygons { get; }
	}
}
