namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolRectangularThermal : OdbSymbolBase
	{
		private OdbSymbolRectangularThermal() { }

		public OdbSymbolRectangularThermal(int index, 
			double outerDiameter, double innerDiameter,
			double angle, int numberOfSpoke, double gap, double airGap) : base(index)
		{
			OuterDiameter = outerDiameter;
			InnerDiameter = innerDiameter;
			Angle = angle;
			NumberOfSpoke = numberOfSpoke;
			Gap = gap;
			AirGap = airGap;
		}

		public double OuterDiameter { get; private set; }

		public double InnerDiameter { get; private set; }

		public double Angle { get; private set; }

		public int NumberOfSpoke { get; private set; }

		public double Gap { get; private set; }

		public double AirGap { get; private set; }

		public static OdbSymbolRectangularThermal Create(int index, string param)
		{
			string[] split = param.Split('X');
			if (split.Length != 6)
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

			if (double.TryParse(split[5], out double airGap) is false)
			{
				return null;
			}

			return new OdbSymbolRectangularThermal(index, outerDiameter, innerDiameter, 
				angle, numberOfSpoke, gap, airGap);
		}
	}
}
