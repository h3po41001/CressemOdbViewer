using System.Drawing;
using CressemExtractLibrary.Data.Odb.Symbol.Interface;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolSquareRoundDonut : OdbSymbolSquare, IOdbSymbolSqureRoundDonut
	{
		private OdbSymbolSquareRoundDonut() { }

		public OdbSymbolSquareRoundDonut(PointF pos, 
			double outerDiameter, double innerDiameter) : base(pos, outerDiameter)
		{
			InnerDiameter = innerDiameter;
		}

		public double InnerDiameter { get; private set; }
	}
}
