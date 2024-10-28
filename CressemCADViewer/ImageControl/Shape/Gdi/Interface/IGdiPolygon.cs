using System.Collections.Generic;
using System.Drawing;

namespace ImageControl.Shape.Gdi.Interface
{
	public interface IGdiPolygon : IGdiShape
	{
		bool IsFill { get; }

		IEnumerable<IGdiShape> Shapes { get; }

		IEnumerable<PointF> Points { get; }
	}
}
