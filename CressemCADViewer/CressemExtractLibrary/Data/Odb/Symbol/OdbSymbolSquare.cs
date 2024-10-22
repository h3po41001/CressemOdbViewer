using CressemExtractLibrary.Data.Interface.Symbol;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolSquare : OdbSymbolRound, ISymbolSquare
	{
		protected OdbSymbolSquare() { }

		public OdbSymbolSquare(int index, double outerDiameter) : base(index, outerDiameter)
		{
		}

		public static new OdbSymbolSquare Create(int index, string param)
		{
			if (double.TryParse(param, out double side) is false)
			{
				return null;
			}

			return new OdbSymbolSquare(index, side);
		}
	}
}
