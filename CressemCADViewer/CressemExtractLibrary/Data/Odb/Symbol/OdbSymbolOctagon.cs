using CressemExtractLibrary.Data.Interface.Symbol;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolOctagon : OdbSymbolRectangle, ISymbolOctagon
	{
		protected OdbSymbolOctagon() { }

		public OdbSymbolOctagon(int index, double width, double height,
			double corner) : base(index, width, height)
		{
			CornerSize = corner;
		}

		public double CornerSize { get; private set; }

		public static new OdbSymbolOctagon Create(int index, string param)
		{
			string[] split = param.Split('X');
			if (split.Length != 3)
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

			if (double.TryParse(split[2], out double corner) is false)
			{
				return null;
			}

			return new OdbSymbolOctagon(index, width, height, corner);
		}
	}
}
