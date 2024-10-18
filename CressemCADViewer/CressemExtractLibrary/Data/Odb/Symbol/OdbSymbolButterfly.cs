namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolButterfly : OdbSymbolBase
	{
		private OdbSymbolButterfly() { }

		public OdbSymbolButterfly(int index, double diameter) : base(index)
		{
			Diameter = diameter;
		}

		public double Diameter { get; private set; }

		public static OdbSymbolButterfly Create(int index, string param)
		{
			if (double.TryParse(param, out double diameter) is false)
			{
				return null;
			}

			return new OdbSymbolButterfly(index, diameter);
		}
	}
}
