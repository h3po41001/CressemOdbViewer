namespace ImageControl.Shape.DirectX.Interface
{
	public interface IDirectLine : IDirectShape
	{
		float Sx { get; }

		float Sy { get; }

		float Ex { get; }

		float Ey { get; }

		float LineWidth { get; }
	}
}
