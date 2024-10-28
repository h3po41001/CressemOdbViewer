using System.Collections.Generic;
using ImageControl.Shape.Interface;

namespace ImageControl.Shape.Gdi.Interface
{
	public interface IGdiList : IGraphicsList
	{
		IEnumerable<IGdiShape> Shapes { get; }
	}
}
