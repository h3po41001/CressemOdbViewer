using System.Collections.Generic;

namespace ImageControl.Shape.Gdi.Interface
{
	public interface IGdiList
	{
		IEnumerable<IGdiBase> Shapes { get; }
	}
}
