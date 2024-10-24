using CressemExtractLibrary.Data.Interface.Symbol;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolSquare : OdbSymbolBase, ISymbolSquare
	{
		protected OdbSymbolSquare() { }

		public OdbSymbolSquare(int index, double side) : base(index)
		{
			Diameter = side;
		}

		public double Diameter { get; private set; }

		public static OdbSymbolSquare Create(int index, string param)
		{
			if (double.TryParse(param, out double side) is false)
			{
				return null;
			}

			return new OdbSymbolSquare(index, side);
		}
	}
}
