namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolRectangularThermal : OdbSymbolBase
	{
		private OdbSymbolRectangularThermal() { }

		public bool IsOpenCorners { get; private set; }

		public static OdbSymbolRectangularThermal Create(string param)
		{
			OdbSymbolRectangularThermal symbol = new OdbSymbolRectangularThermal();
			symbol.IsOpenCorners = param == "1";
			return symbol;
		}
	}
}
