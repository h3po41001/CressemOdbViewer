using System.Collections.Generic;

namespace ImageControl.Shape.Interface
{
	public interface IShapePolygon : IShapeBase
	{
		bool IsFill { get; }

		IEnumerable<IShapeBase> Shapes { get; }
	}
}
