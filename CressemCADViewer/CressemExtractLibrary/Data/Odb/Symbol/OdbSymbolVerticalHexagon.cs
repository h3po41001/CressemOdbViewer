namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolVerticalHexagon : OdbSymbolBase
	{
		private OdbSymbolVerticalHexagon()
		{
		}

		public OdbSymbolVerticalHexagon(int index, 
			double width, double height, double cornerSize) : base(index)
		{
			Width = width;
			Height = height;
			CornerSize = cornerSize;
		}

		public double Width { get; private set; }

		public double Height { get; private set; }

		public double CornerSize { get; private set; }

		public static OdbSymbolVerticalHexagon Create(int index, string param)
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

			if (double.TryParse(split[2], out double cornerSize) is false)
			{
				return null;
			}

			return new OdbSymbolVerticalHexagon(index, width, height, cornerSize);
		}
	}
}
