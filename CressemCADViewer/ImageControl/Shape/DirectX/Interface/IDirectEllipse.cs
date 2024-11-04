using ImageControl.Shape.Interface;

namespace ImageControl.Shape.DirectX.Interface
{
	public interface IDirectEllipse : IGraphicsShape
	{
		float Cx { get; }

		float Cy { get; }

		float RadiusX { get; }

		float RadiusY { get; }
	}
}
