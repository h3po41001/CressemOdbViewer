using System.Drawing;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolSquare : OdbSymbolBase
	{
		private OdbSymbolSquare() { }

		public OdbSymbolSquare(PointF pos, double side) : base(pos)
		{
			Side = side;
		}

		public double Side { get; private set; }
	}
}
