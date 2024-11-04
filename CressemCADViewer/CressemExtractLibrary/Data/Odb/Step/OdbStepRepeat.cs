using CressemExtractLibrary.Data.Interface.Step;

namespace CressemExtractLibrary.Data.Odb.Step
{
	internal class OdbStepRepeat : IRepeatInfo
	{
		public OdbStepRepeat(string name,
			double x, double y,
			double dx, double dy,
			int nx, int ny, double angle,
			bool isFlipHorizontal)
		{
			Name = name;
			Sx = x;
			Sy = y;
			Dx = dx;
			Dy = dy;
			Nx = nx;
			Ny = ny;
			Angle = angle;
			IsFlipHorizontal = isFlipHorizontal;
		}

		public string Name { get; private set; }

		public double Sx { get; private set; }

		public double Sy { get; private set; }

		public double Dx { get; private set; }

		public double Dy { get; private set; }

		public int Nx { get; private set; }

		public int Ny { get; private set; }

		public double Angle { get; private set; }

		public bool IsFlipHorizontal { get; private set; }
	}
}
