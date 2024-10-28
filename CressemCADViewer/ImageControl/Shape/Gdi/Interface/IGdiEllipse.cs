namespace ImageControl.Shape.Gdi.Interface
{
	public interface IGdiEllipse : IGdiShape
	{
		float Sx { get; }

		float Sy { get; }

		float Width { get; }

		float Height { get; }
	}
}
