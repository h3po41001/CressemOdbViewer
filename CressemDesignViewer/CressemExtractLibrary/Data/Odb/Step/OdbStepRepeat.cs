using System.Drawing;
using CressemExtractLibrary.Convert;

namespace CressemExtractLibrary.Data.Odb.Step
{
	internal class OdbStepRepeat
	{
		public OdbStepRepeat(string name,
			PointF x, PointF y,
			PointF dx, PointF dy,
			Point nx, Point ny, double angle,
			string flip, string mirror)
		{
			Name = name;
			X = x;
			Y = y;
			DX = dx;
			DY = dy;
			NX = nx;
			NY = ny;
			Angle = angle;
			Flip = Converter.Instance.ConvertToYesOrNo(flip);
			Mirror = Converter.Instance.ConvertToYesOrNo(mirror);
		}

		public string Name { get; private set; }

		public PointF X { get; private set; }

		public PointF Y { get; private set; }

		public PointF DX { get; private set; }

		public PointF DY { get; private set; }

		public Point NX { get; private set; }

		public Point NY { get; private set; }

		public double Angle { get; private set; }

		public YesOrNo Flip { get; private set; }

		public YesOrNo Mirror { get; private set; }


	}
}
