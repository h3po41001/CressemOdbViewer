namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolRoundedSquareThermal : OdbSymbolBase
	{
		private OdbSymbolRoundedSquareThermal() { }

		public bool IsOpenCorners { get; private set; }

		public static OdbSymbolRoundedSquareThermal Create(string param)
		{
			OdbSymbolRoundedSquareThermal symbol = new OdbSymbolRoundedSquareThermal();
			symbol.IsOpenCorners = param == "1";
			return symbol;
		}
	}
}
