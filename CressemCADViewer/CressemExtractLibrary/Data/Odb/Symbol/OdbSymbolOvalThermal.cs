namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolOvalThermal : OdbSymbolBase
	{
		private OdbSymbolOvalThermal() { }

		public bool IsOpenCorners { get; private set; }

		public static OdbSymbolOvalThermal Create(string param)
		{
			OdbSymbolOvalThermal symbol = new OdbSymbolOvalThermal();
			symbol.IsOpenCorners = param == "1";
			return symbol;
		}
	}
}
