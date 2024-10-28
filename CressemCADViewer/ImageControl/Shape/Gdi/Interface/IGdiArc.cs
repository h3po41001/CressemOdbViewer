namespace ImageControl.Shape.Gdi.Interface
{
	public interface IGdiArc : IGdiShape
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
