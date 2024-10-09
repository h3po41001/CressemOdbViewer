namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolSquareThermal : OdbSymbolBase
	{
		private OdbSymbolSquareThermal() { }

		public bool IsOpenCorners { get; private set; }

		public static OdbSymbolSquareThermal Create(string param)
		{
			OdbSymbolSquareThermal symbol = new OdbSymbolSquareThermal();
			symbol.IsOpenCorners = param == "1";
			return symbol;
		}
	}
}
