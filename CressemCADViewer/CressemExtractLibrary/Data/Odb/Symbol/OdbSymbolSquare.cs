using System.Drawing;
using CressemExtractLibrary.Data.Odb.Symbol.Interface;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolSquare : OdbSymbolBase, IOdbSymbolSqure
	{
		protected OdbSymbolSquare() { }

		public OdbSymbolSquare(PointF pos, double outerDiameter) : base(pos)
		{
			OuterDiameter = outerDiameter;
		}

		public double OuterDiameter { get; private set; }
	}
}
