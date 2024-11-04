using ImageControl.Shape.Interface;

namespace ImageControl.Shape.Gdi.Interface
{
	public interface IGdiLine : IGraphicsShape
	{
		float Sx { get; }

		float Sy { get; }

		float Ex { get; }

		float Ey { get; }

		float LineWidth { get; }
	}
}
