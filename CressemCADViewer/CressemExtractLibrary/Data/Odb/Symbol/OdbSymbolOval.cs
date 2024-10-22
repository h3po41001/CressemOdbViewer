using CressemExtractLibrary.Data.Interface.Symbol;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolOval : OdbSymbolRectangle, ISymbolOval
	{
		protected OdbSymbolOval() { }

		public OdbSymbolOval(int index, double width, double height) : 
			base(index, width, height)
		{
		}

		public static new OdbSymbolOval Create(int index, string param)
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

			return new OdbSymbolOval(index, width, height);
		}
	}
}
