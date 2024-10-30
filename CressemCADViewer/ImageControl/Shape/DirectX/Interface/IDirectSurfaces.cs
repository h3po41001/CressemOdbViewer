using System.Collections.Generic;

namespace ImageControl.Shape.DirectX.Interface
{
	public interface IDirectSurfaces : IDirectShape
	{
		IEnumerable<IDirectSurface> Surfaces { get; }
	}
}
