using System.Drawing;
using CressemExtractLibrary.Convert;

namespace CressemExtractLibrary.Data.Odb.Step
{
	internal class OdbStepRepeat
	{
		public OdbStepRepeat(string name,
			double x, double y,
			double dx, double dy,
			int nx, int ny, double angle,
			bool isFlip, bool isMirrored)
		{
			Name = name;
			X = x;
			Y = y;
			DX = dx;
			DY = dy;
			NX = nx;
			NY = ny;
			Angle = angle;
			IsFliped = isFlip;
			IsMirrored = isMirrored;
		}

		public string Name { get; private set; }

		public double X { get; private set; }

		public double Y { get; private set; }

		public double DX { get; private set; }

		public double DY { get; private set; }

		public int NX { get; private set; }

		public int NY { get; private set; }

		public double Angle { get; private set; }

		public bool IsFliped { get; private set; }

		public bool IsMirrored { get; private set; }
	}
}
