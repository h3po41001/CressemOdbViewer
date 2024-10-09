using System.Drawing;
using CressemExtractLibrary.Data.Odb.Symbol.Interface;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolRoundDonut : OdbSymbolRound, IOdbSymbolRoundDonut
	{
		private OdbSymbolRoundDonut() { }

		public OdbSymbolRoundDonut(double outerDiameter, 
			double innerDiameter) : base(outerDiameter)
		{
			InnerDiameter = innerDiameter;
		}

		public double InnerDiameter { get; private set; }

		public static new OdbSymbolRoundDonut Create(string param)
		{
			return null;
		}
	}
}
