using ImageControl.Shape.Interface;

namespace ImageControl.Shape.Gdi.Interface
{
	public interface IGdiRectangle : IGraphicsShape
	{
		float X { get; }

		float Y { get; }

		float Width { get; }

		float Height { get; }
	}
}
