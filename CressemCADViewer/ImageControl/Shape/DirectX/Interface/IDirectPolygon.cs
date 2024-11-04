using System.Collections.Generic;
using System.Drawing;
using ImageControl.Shape.Interface;

namespace ImageControl.Shape.DirectX.Interface
{
	public interface IDirectPolygon : IGraphicsShape
	{
		bool IsFill { get; }

		IEnumerable<IGraphicsShape> Shapes { get; }

		IEnumerable<PointF> Points { get; }
	}
}
