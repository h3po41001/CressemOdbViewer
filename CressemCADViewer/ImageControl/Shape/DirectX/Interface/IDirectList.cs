using System.Collections.Generic;

namespace ImageControl.Shape.DirectX.Interface
{
	internal interface IDirectList
	{
		IEnumerable<IDirectShape> Shapes { get; }
	}
}
