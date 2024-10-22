using CressemExtractLibrary.Data.Interface.Symbol;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolHole : OdbSymbolRound, ISymbolHole
	{
		private OdbSymbolHole() { }

		public OdbSymbolHole(int index, 
			double diameter, string platingStatus, 
			double plusTolerance, double minusTolerance) : base(index, diameter)
		{
			PlatingStatus = platingStatus;
			PlusTolerance = plusTolerance;
			MinusTolerance = minusTolerance;
		}

		public string PlatingStatus { get; private set; }

		public double PlusTolerance { get; private set; }

		public double MinusTolerance { get; private set; }

		public static new OdbSymbolHole Create(int index, string param)
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

			return new OdbSymbolHole(index, diameter, 
				split[1], plusTolerance, minusTolerance);
		}
	}
}
