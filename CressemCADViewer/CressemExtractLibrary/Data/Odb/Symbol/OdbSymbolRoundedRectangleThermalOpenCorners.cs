﻿namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolRoundedRectangleThermalOpenCorners : OdbSymbolEditedCorner
	{
		private OdbSymbolRoundedRectangleThermalOpenCorners() { }

		public OdbSymbolRoundedRectangleThermalOpenCorners(double outerDiameter, double innerDiameter,
			double angle, int numberOfSpoke, double gap,
			double cornerRadius, string corners) : base(cornerRadius, corners, numberOfSpoke)
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

		public static OdbSymbolRoundedRectangleThermalOpenCorners Create(string param)
		{
			string[] split = param.Split('X');
			if (split.Length != 7)
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

			if (double.TryParse(split[5], out double cornerRadius) is false)
			{
				return null;
			}

			return new OdbSymbolRoundedRectangleThermalOpenCorners(outerDiameter, innerDiameter,
				angle, numberOfSpoke, gap, cornerRadius, split[6]);
		}
	}
}