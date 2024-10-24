using CressemExtractLibrary.Data.Interface.Symbol;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolRoundedRectangleDonut : OdbSymbolEditedCorner, ISymbolRoundedRectangleDonut
	{
		private OdbSymbolRoundedRectangleDonut() { }

		public OdbSymbolRoundedRectangleDonut(int index, 
			double width, double height,
			double lineWidth, double cornerRadius, 
			string corners) : base(index, cornerRadius, corners, 4) 
		{
			Width = width;
			Height = height;
			LineWidth = lineWidth;
		}

		public double Width { get; private set; }

		public double Height { get; private set; }

		public double LineWidth { get; private set; }

		public static OdbSymbolRoundedRectangleDonut Create(int index, string param)
		{
			string[] split = param.Split('X');
			if (split.Length != 5)
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

			if (double.TryParse(split[2], out double lineWidth) is false)
			{
				return null;
			}

			if (double.TryParse(split[3], out double cornerRadius) is false)
			{
				return null;
			}

			return new OdbSymbolRoundedRectangleDonut(index, width, height, 
				lineWidth, cornerRadius, split[4]);
		}
	}
}
