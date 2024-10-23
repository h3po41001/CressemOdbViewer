using System.Collections.Generic;

namespace ImageControl.Shape.Interface
{
	public interface IShapeList
	{
		float Xdatum { get; }

		float Ydatum { get; }

		int Orient { get; }

		IEnumerable<IShapeBase> Shapes { get; }
	}
}
