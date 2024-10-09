namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolRoundedRectangleThermal : OdbSymbolBase
	{
		private OdbSymbolRoundedRectangleThermal() { }

		public bool IsOpenCorners { get; private set; }

		public static OdbSymbolRoundedRectangleThermal Create(string param)
		{
			OdbSymbolRoundedRectangleThermal symbol = new OdbSymbolRoundedRectangleThermal();
			symbol.IsOpenCorners = param == "1";
			return symbol;
		}
	}
}
