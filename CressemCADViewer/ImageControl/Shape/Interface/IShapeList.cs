using System.Collections.Generic;

namespace ImageControl.Shape.Interface
{
	public interface IShapeList
	{
		IEnumerable<IShapeBase> Shapes { get; }
	}
}
