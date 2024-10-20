using System.Collections.Generic;

namespace ImageControl.Shape.Interface
{
	public interface IShapePolygon : IShapeBase
	{
		IEnumerable<IShapeBase> Shapes { get; set; }
	}
}
