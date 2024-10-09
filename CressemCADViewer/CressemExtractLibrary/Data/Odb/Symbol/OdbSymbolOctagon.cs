using System.Drawing;
using CressemExtractLibrary.Data.Odb.Symbol.Interface;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolOctagon : OdbSymbolRectangle, IOdbSymbolOctagon
	{
		protected OdbSymbolOctagon() { }

		public OdbSymbolOctagon(double width, double height, 
			double corner) : base(width, height) 
		{
			CornerSize = corner;
		}

		public double CornerSize { get; private set; }

		public static new OdbSymbolOctagon Create(string param)
		{
			return null;
		}
	}
}
