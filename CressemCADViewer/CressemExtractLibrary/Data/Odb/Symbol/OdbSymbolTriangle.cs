namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolTriangle : OdbSymbolBase
	{
		private OdbSymbolTriangle()
		{
		}

		public OdbSymbolTriangle(double triangleBase, double height) : base()
		{
			TriangleBase = triangleBase;
			Height = height;
		}

		public double TriangleBase { get; private set; }

		public double Height { get; private set; }

		public static OdbSymbolTriangle Create(string param)
		{
			string[] split = param.Split('X');
			if (split.Length != 2)
			{
				return null;
			}

			if (double.TryParse(split[0], out double triangleBase) is false)
			{
				return null;
			}

			if (double.TryParse(split[1], out double height) is false)
			{
				return null;
			}

			return new OdbSymbolTriangle(triangleBase, height);
		}
	}
}
