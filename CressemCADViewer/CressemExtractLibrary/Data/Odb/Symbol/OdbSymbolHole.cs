﻿namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolHole : OdbSymbolBase
	{
		private OdbSymbolHole() { }

		public OdbSymbolHole(double diameter, string platingStatus, double plusTolerance, double minusTolerance) : base()
		{
			Diameter = diameter;
			PlatingStatus = platingStatus;
			PlusTolerance = plusTolerance;
			MinusTolerance = minusTolerance;
		}

		public double Diameter { get; private set; }

		public string PlatingStatus { get; private set; }

		public double PlusTolerance { get; private set; }

		public double MinusTolerance { get; private set; }

		public static OdbSymbolHole Create(string param)
		{
			string[] split = param.Split('X');
			if (split.Length != 4)
			{
				return null;
			}

			if (double.TryParse(split[0], out double diameter) is false)
			{
				return null;
			}

			if (double.TryParse(split[2], out double plusTolerance) is false)
			{
				return null;
			}

			if (double.TryParse(split[3], out double minusTolerance) is false)
			{
				return null;
			}

			return new OdbSymbolHole(diameter, split[1], plusTolerance, minusTolerance);
		}
	}
}