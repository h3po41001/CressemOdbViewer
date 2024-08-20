using System.Drawing;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolRound : OdbSymbolBase
	{
		private OdbSymbolRound()
		{
		}

		public OdbSymbolRound(PointF pos, double diameter) : base(pos)
		{
			Diameter = diameter;
		}

		public double Diameter { get; private set; }
	}
}
