namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolHorizontalHexagon : OdbSymbolBase
	{
		private OdbSymbolHorizontalHexagon() { }

		public OdbSymbolHorizontalHexagon(double width, double height, 
			double cornerSize) : base()
		{
			Width = width;
			Height = height;
			CornerSize = cornerSize;
		}

		public double Width { get; private set; }

		public double Height { get; private set; }

		public double CornerSize { get; private set; }

		public static OdbSymbolHorizontalHexagon Create(string param)
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

			return new OdbSymbolHorizontalHexagon(width, height, cornerSize);
		}
	}
}
