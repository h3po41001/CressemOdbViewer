using System.Collections.Generic;
using System.Drawing;

namespace ImageControl.Shape.DirectX.Interface
{
	public interface IDirectPolygon : IDirectShape
	{
		bool IsFill { get; }

		IEnumerable<IDirectShape> Shapes { get; }

		IEnumerable<PointF> Points { get; }
	}
}
