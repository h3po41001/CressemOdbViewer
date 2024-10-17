using System.Drawing;
using System.Linq;
using CressemExtractLibrary.Data.Odb.Symbol.Interface;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolRoundedRectangle : OdbSymbolEditedCorner, IOdbSymbolRoundedRectangle
	{
		protected OdbSymbolRoundedRectangle() { }

		public OdbSymbolRoundedRectangle(double width, double height,
			double cornerRadius, string corners) : base(cornerRadius, corners, 4)
		{
			Width = width;
			Height = height;
		}

		public double Width { get; private set; }

		public double Height { get; private set; }

		public bool[] IsRounded { get => IsEditedCorner; }

		public static new OdbSymbolRoundedRectangle Create(string param)
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

			return new OdbSymbolRoundedRectangle(
				width, height, cornerRadius, value[3]);
		}
	}
}
