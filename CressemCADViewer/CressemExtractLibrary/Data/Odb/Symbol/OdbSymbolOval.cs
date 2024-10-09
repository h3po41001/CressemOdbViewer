using System.Drawing;
using CressemExtractLibrary.Data.Odb.Symbol.Interface;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolOval : OdbSymbolRectangle, IOdbSymbolOval
	{
		protected OdbSymbolOval() { }

		public OdbSymbolOval(double width, double height) : 
			base(width, height)
		{
		}

		public static new OdbSymbolOval Create(string param)
		{
			return null;
		}
	}
}
