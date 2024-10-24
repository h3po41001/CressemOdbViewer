﻿using CressemExtractLibrary.Data.Interface.Symbol;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolRoundedSqureDonut : OdbSymbolEditedCorner, ISymbolRoundedSqureDonut
	{
		private OdbSymbolRoundedSqureDonut() { }

		public OdbSymbolRoundedSqureDonut(int index,
			double outerDiameter, double innerDiameter, 
			double cornerRad, string corners) : base(index, cornerRad, corners, 4)
		{
			Diameter = outerDiameter;
			InnerDiameter = innerDiameter;
		}

		public double Diameter { get; private set; }

		public double InnerDiameter { get; private set; }

		public double Radius { get; private set; }

		public bool[] IsRounded { get => IsEditedCorner; }

		public static OdbSymbolRoundedSqureDonut Create(int index, string param)
		{
			string[] split = param.Split('X');
			if (split.Length != 4)
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

			if (double.TryParse(split[2], out double cornerRad) is false)
			{
				return null;
			}

			return new OdbSymbolRoundedSqureDonut(index,
				outerDiameter, innerDiameter, cornerRad, split[3]);
		}
	}
}
