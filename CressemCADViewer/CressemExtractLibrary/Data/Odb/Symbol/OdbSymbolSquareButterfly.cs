namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolSquareButterfly : OdbSymbolBase
	{
		private OdbSymbolSquareButterfly() { }

		public OdbSymbolSquareButterfly(int index, double diameter) : base(index)
		{
			Diameter = diameter;
		}

		public double Diameter { get; private set; }

		public static OdbSymbolSquareButterfly Create(int index, string param)
		{
			if (double.TryParse(param, out double diameter) is false)
			{
				return null;
			}

			return new OdbSymbolSquareButterfly(index, diameter);
		}
	}
}
