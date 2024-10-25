using System.Collections.Generic;

namespace ImageControl.Shape.DirectX.Interface
{
	public interface IDirectList
	{
		IEnumerable<IDirectShape> Shapes { get; }
	}
}
