using System.Drawing;
using CressemExtractLibrary.Data.Odb.Symbol.Interface;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolSquare : OdbSymbolBase, IOdbSymbolSqure
	{
		protected OdbSymbolSquare() { }

		public OdbSymbolSquare(double outerDiameter) : base()
		{
			OuterDiameter = outerDiameter;
		}

		public double OuterDiameter { get; private set; }

		public static OdbSymbolSquare Create(string param)
		{
			if (double.TryParse(param, out double side) is false)
			{
				return null;
			}

			return new OdbSymbolSquare(side);
		}
	}
}
