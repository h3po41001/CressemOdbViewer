using System.Collections.Generic;

namespace ImageControl.Shape.Interface
{
	public interface IGraphicsList
	{
		bool IsPositive { get; }

		IEnumerable<IGraphicsShape> Shapes { get; }
	}
}
