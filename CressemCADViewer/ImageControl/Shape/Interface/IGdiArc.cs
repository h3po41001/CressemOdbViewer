namespace ImageControl.Shape.Interface
{
	public interface IGdiArc : IGdiBase
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
