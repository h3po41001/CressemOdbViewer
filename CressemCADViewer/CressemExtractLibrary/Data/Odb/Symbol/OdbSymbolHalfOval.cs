namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolHalfOval : OdbSymbolBase
	{
		private OdbSymbolHalfOval() { }

		public OdbSymbolHalfOval(double width, double height) : base()
		{
			Width = width;
			Height = height;
		}

		public double Width { get; private set; }

		public double Height { get; private set; }

		public static OdbSymbolHalfOval Create(string param)
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

			return new OdbSymbolHalfOval(width, height);
		}
	}
}
