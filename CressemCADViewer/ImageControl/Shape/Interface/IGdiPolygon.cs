using System.Collections.Generic;
using System.Drawing;

namespace ImageControl.Shape.Interface
{
	public interface IGdiPolygon : IGdiBase
	{
		bool IsFill { get; }

		IEnumerable<IGdiBase> Shapes { get; }

		IEnumerable<PointF> Points { get; }
	}
}
