using System.Drawing;
using CressemExtractLibrary.Data.Odb.Symbol.Interface;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolRoundDonut : OdbSymbolRound, IOdbSymbolRoundDonut
	{
		private OdbSymbolRoundDonut() { }

		public OdbSymbolRoundDonut(PointF pos, 
			double outerDiameter, double innerDiameter) : base(pos, outerDiameter)
		{
			InnerDiameter = innerDiameter;
		}

		public double InnerDiameter { get; private set; }
	}
}
