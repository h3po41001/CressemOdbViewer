namespace ImageControl.Shape.Gdi.Interface
{
	public interface IGdiRectangle : IGdiShape
	{
		float X { get; }

		float Y { get; }

		float Width { get; }

		float Height { get; }
	}
}
