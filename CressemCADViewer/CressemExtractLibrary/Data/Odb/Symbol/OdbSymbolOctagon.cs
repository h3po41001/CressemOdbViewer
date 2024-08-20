using System.Drawing;
using CressemExtractLibrary.Data.Odb.Symbol.Interface;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolOctagon : OdbSymbolRectangle, IOdbSymbolOctagon
	{
		protected OdbSymbolOctagon() { }

		public OdbSymbolOctagon(PointF pos, 
			double width, double height, double corner) : base(pos, width, height) 
		{
			CornerSize = corner;
		}

		public double CornerSize { get; private set; }
	}
}
