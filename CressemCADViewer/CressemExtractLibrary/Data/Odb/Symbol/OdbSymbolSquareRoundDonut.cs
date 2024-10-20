namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolSquareRoundDonut : OdbSymbolSquare
	{
		private OdbSymbolSquareRoundDonut() { }

		public OdbSymbolSquareRoundDonut(int index, double outerDiameter, 
			double innerDiameter) : base(index, outerDiameter)
		{
			InnerDiameter = innerDiameter;
		}

		public double InnerDiameter { get; private set; }

		public static new OdbSymbolSquareRoundDonut Create(int index, string param)
		{
			string[] split = param.Split('X');
			if (split.Length != 2)
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

			return new OdbSymbolSquareRoundDonut(index, outerDiameter, innerDiameter);
		}
	}
}
