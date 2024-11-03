using ImageControl.Shape.Interface;

namespace ImageControl.Shape.DirectX.Interface
{
	public interface IDirectRectangle : IGraphicsShape
	{
		float Left { get; }

		float Top { get; }

		float Right { get; }

		float Bottom { get; }
	}
}
