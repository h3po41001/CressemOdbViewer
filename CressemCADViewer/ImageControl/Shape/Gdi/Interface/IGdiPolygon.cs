using System.Collections.Generic;
using System.Drawing;
using ImageControl.Shape.Interface;

namespace ImageControl.Shape.Gdi.Interface
{
	public interface IGdiPolygon : IGraphicsShape
	{
		bool IsFill { get; }

		IEnumerable<IGraphicsShape> Shapes { get; }

		IEnumerable<PointF> Points { get; }
	}
}
