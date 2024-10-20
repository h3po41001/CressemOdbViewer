namespace ImageControl.Shape.Interface
{
	public interface IShapeRectangle : IShapeBase
	{
		float X { get; set; }

		float Y { get; set; }

		float Width { get; set; }

		float Height { get; set; }
	}
}
