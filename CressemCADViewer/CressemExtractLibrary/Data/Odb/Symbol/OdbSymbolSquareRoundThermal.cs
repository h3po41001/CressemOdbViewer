﻿namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolSquareRoundThermal : OdbSymbolBase
	{
		private OdbSymbolSquareRoundThermal()
		{
		}

		public OdbSymbolSquareRoundThermal(double outerDiameter, double innerDiameter,
			double angle, int numberOfSpoke, double gap) : base()
		{
			OuterDiameter = outerDiameter;
			InnerDiameter = innerDiameter;
			Angle = angle;
			NumberOfSpoke = numberOfSpoke;
			Gap = gap;
		}

		public double OuterDiameter { get; private set; }

		public double InnerDiameter { get; private set; }

		public double Angle { get; private set; }

		public int NumberOfSpoke { get; private set; }

		public double Gap { get; private set; }

		public static OdbSymbolSquareRoundThermal Create(string param)
		{
			string[] split = param.Split('X');
			if (split.Length != 5)
			{
				return null;
			}

			if (double.TryParse(split[0], out double outerDiameter) is false)
			{
				return null;
			}

			if (double.TryParse(split[1], out double innerDiameter) is false)
			{
				return null;
			}

			if (double.TryParse(split[2], out double angle) is false)
			{
				return null;
			}

			if (int.TryParse(split[3], out int numberOfSpoke) is false)
			{
				return null;
			}

			if (double.TryParse(split[4], out double gap) is false)
			{
				return null;
			}

			return new OdbSymbolSquareRoundThermal(
				outerDiameter, innerDiameter, angle, numberOfSpoke, gap);
		}
	}
}