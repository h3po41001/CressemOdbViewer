using System.Drawing;
using System.Linq;
using CressemExtractLibrary.Data.Odb.Symbol.Interface;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolChamferedRectangle : OdbSymbolEditedCorner, IOdbSymbolChamferedRectangle
	{
		private OdbSymbolChamferedRectangle() { }

		public OdbSymbolChamferedRectangle(double width, double height,
			double cornerRadius, string corners) : base(cornerRadius, corners, 4)
		{
			Width = width;
			Height = height;
		}

		public double Width { get; private set; }

		public double Height { get; private set; }

		// RT = 0, LT = 1, LB = 2, RB = 3
		public bool[] IsChamfered { get => IsEditedCorner; }

		public static new OdbSymbolChamferedRectangle Create(string param)
		{
			string[] split = param.Split('X');
			if (split.Length != 4)
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

			if (double.TryParse(string.Concat(split[2].Skip(1)), 
				out double cornerRadius) is false)
			{
				return null;
			}

			return new OdbSymbolChamferedRectangle(
				width, height, cornerRadius, split[3]);
		}
	}
}
