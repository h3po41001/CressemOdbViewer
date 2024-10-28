using System.Collections.Generic;
using ImageControl.Shape.Interface;

namespace ImageControl.Shape.DirectX.Interface
{
	public interface IDirectList : IGraphicsList
	{
		IEnumerable<IDirectShape> Shapes { get; }
	}
}
