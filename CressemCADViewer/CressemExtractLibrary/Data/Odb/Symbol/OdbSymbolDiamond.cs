using System.Drawing;
using CressemExtractLibrary.Data.Odb.Symbol.Interface;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolDiamond : OdbSymbolRectangle, IOdbSymbolDiamond
	{
		protected OdbSymbolDiamond() { }

		public OdbSymbolDiamond(double width, double height) : 
			base(width, height)
		{
		}

		public static new OdbSymbolDiamond Create(string param)
		{
			return null;
		}
	}
}
