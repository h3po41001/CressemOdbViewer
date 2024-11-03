using ImageControl.Shape.Interface;

namespace ImageControl.Shape.Gdi.Interface
{
	public interface IGdiEllipse : IGraphicsShape
	{
		float Sx { get; }

		float Sy { get; }

		float Width { get; }

		float Height { get; }
	}
}
