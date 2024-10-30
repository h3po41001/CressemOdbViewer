namespace ImageControl.Shape.DirectX.Interface
{
	public interface IDirectArc : IDirectShape
	{
		float Sx { get; }

		float Sy { get; }

		float Ex { get; }

		float Ey { get; }

		float RadiusWidth { get; }

		float RadiusHeight { get; }

		float Rotation { get; }

		bool IsLargeArc { get; }

		bool IsClockwise { get; }

		float LineWidth { get; }
	}
}
