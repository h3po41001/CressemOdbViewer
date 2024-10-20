namespace ImageControl.Shape.Interface
{
	public interface IShapeArc : IShapeBase
	{
		float Sx { get; set; }

		float Sy { get; set; }

		float Ex { get; set; }

		float Ey { get; set; }

		float Cx { get; set; }

		float Cy { get; set; }

		bool IsClockWise { get; set; }
		//float X { set; }

		//float Y { set; }

		//float Width { set; }

		//float Height { set; }

		//float StartAngle { set; }

		//float SweepAngle { set; }
	}
}
