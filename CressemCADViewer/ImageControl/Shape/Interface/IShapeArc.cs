namespace ImageControl.Shape.Interface
{
	public interface IShapeArc : IShapeBase
	{
		float X { get; }

		float Y { get; }

		float Width { get; }

		float Height { get; }

		float StartAngle { get; }

		float SweepAngle { get; }

		float LineWidth { get; }
	}
}
