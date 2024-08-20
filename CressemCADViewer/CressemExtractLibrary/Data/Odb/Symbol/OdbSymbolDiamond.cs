using System.Drawing;
using CressemExtractLibrary.Data.Odb.Symbol.Interface;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolDiamond : OdbSymbolRectangle, IOdbSymbolDiamond
	{
		protected OdbSymbolDiamond() { }

		public OdbSymbolDiamond(PointF pos, 
			double width, double height) : base(pos, width, height)
		{
		}
	}
}
