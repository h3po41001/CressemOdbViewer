using System.Linq;
using CressemExtractLibrary.Data.Interface.Symbol;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolRoundedRectangle : OdbSymbolEditedCorner, ISymbolRoundedRectangle
	{
		protected OdbSymbolRoundedRectangle() { }

		public OdbSymbolRoundedRectangle(int index, 
			double width, double height,
			double cornerRadius, string corners) : base(index, cornerRadius, corners, 4)
		{
			Width = width;
			Height = height;
		}

		public double Width { get; private set; }

		public double Height { get; private set; }

		public bool[] IsRounded { get => IsEditedCorner; }

		public static OdbSymbolRoundedRectangle Create(int index, string param)
		{
			string[] value = param.Split('X').ToArray();
			if (value.Length != 4)
			{
				return null;
			}

			if (double.TryParse(value[0], out double width) is false)
			{
				return null;
			}

			if (double.TryParse(value[1], out double height) is false)
			{
				return null;
			}

			if (double.TryParse(string.Concat(value[2].Skip(1)),
				out double cornerRadius) is false)
			{
				return null;
			}

			return new OdbSymbolRoundedRectangle(index,	width, height, 
				cornerRadius, value[3]);
		}
	}
}
