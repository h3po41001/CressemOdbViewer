using System.Drawing;
using CressemExtractLibrary.Data.Odb.Symbol.Interface;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolSquareRoundDonut : OdbSymbolSquare, IOdbSymbolSqureRoundDonut
	{
		private OdbSymbolSquareRoundDonut() { }

		public OdbSymbolSquareRoundDonut(double outerDiameter, 
			double innerDiameter) : base(outerDiameter)
		{
			InnerDiameter = innerDiameter;
		}

		public double InnerDiameter { get; private set; }

		public static new OdbSymbolSquareRoundDonut Create(string param)
		{
			return null;
		}
	}
}
