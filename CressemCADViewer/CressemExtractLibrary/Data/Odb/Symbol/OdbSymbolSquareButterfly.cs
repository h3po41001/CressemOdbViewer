namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolSquareButterfly : OdbSymbolBase
	{
		private OdbSymbolSquareButterfly() { }

		public OdbSymbolSquareButterfly(double diameter) : base()
		{
			Diameter = diameter;
		}

		public double Diameter { get; private set; }

		public static OdbSymbolSquareButterfly Create(string param)
		{
			if (double.TryParse(param, out double diameter) is false)
			{
				return null;
			}

			return new OdbSymbolSquareButterfly(diameter);
		}
	}
}
