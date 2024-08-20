using System.Drawing;
using CressemExtractLibrary.Data.Odb.Symbol.Interface;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolRound : OdbSymbolBase, IOdbSymbolRound
	{
		protected OdbSymbolRound()
		{
		}

		public OdbSymbolRound(PointF pos, double diameter) : base(pos)
		{
			Diameter = diameter;
		}

		public double Diameter { get; private set; }
	}
}
