namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolRoundedThermal : OdbSymbolBase
	{
		private OdbSymbolRoundedThermal() { }

		public bool IsRounded { get; private set; }

		public static OdbSymbolRoundedThermal Create(string param)
		{
			OdbSymbolRoundedThermal symbol = new OdbSymbolRoundedThermal();
			symbol.IsRounded = param == "1";
			return symbol;
		}
	}
}
