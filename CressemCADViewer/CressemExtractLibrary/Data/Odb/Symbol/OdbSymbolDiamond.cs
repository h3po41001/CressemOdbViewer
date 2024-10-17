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
			string[] split = param.Split('X');
			if (split.Length != 2)
			{
				return null;
			}

			if (double.TryParse(split[0], out double width) is false)
			{
				return null;
			}

			if (double.TryParse(split[1], out double height) is false)
			{
				return null;
			}

			return new OdbSymbolDiamond(width, height);
		}
	}
}
