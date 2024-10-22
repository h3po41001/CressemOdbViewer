using System.Collections.Generic;
using System.Drawing;

namespace ImageControl.Shape.Interface
{
	public interface IShapePolygon : IShapeBase
	{
		bool IsFill { get; }

		IEnumerable<IShapeBase> Shapes { get; }

		IEnumerable<PointF> Points { get; }
	}
}
