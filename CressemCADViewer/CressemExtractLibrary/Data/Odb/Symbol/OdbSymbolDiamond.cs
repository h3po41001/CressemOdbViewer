using CressemExtractLibrary.Data.Interface.Symbol;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolDiamond : OdbSymbolRectangle, ISymbolRectangle
	{
		protected OdbSymbolDiamond() { }

		public OdbSymbolDiamond(int index, double width, double height) : 
			base(index, width, height)
		{
		}

		public static new OdbSymbolDiamond Create(int index, string param)
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

			return new OdbSymbolDiamond(index, width, height);
		}
	}
}
