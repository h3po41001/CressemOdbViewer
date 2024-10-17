namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolButterfly : OdbSymbolBase
	{
		private OdbSymbolButterfly() { }

		public OdbSymbolButterfly(double diameter) : base()
		{
			Diameter = diameter;
		}

		public double Diameter { get; private set; }

		public static OdbSymbolButterfly Create(string param)
		{
			if (double.TryParse(param, out double diameter) is false)
			{
				return null;
			}

			return new OdbSymbolButterfly(diameter);
		}
	}
}
