namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolEllipse : OdbSymbolBase
	{
		private OdbSymbolEllipse() { }

		public OdbSymbolEllipse(int index, double width, double height) : base(index)
		{
			Width = width;
			Height = height;
		}

		public double Width { get; private set; }

		public double Height { get; private set; }

		public static OdbSymbolEllipse Create(int index, string param)
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

			return new OdbSymbolEllipse(index, width, height);
		}
	}
}
