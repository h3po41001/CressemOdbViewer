namespace ImageControl.Shape.Gdi.Interface
{
	public interface IGdiLine : IGdiBase
	{
		float Sx { get; }

		float Sy { get; }

		float Ex { get; }

		float Ey { get; }

		float LineWidth { get; }
	}
}
