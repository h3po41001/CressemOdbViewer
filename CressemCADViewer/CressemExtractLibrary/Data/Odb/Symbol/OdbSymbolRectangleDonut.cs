using System.Drawing;
using CressemExtractLibrary.Data.Odb.Symbol.Interface;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolRectangleDonut : OdbSymbolRectangle, IOdbSymbolRectangleDonut
	{
		protected OdbSymbolRectangleDonut()
		{
		}

		public OdbSymbolRectangleDonut(double width, double height, 
			double lineWidth) : base(width, height)
		{
			LineWidth = lineWidth;
		}

		public double LineWidth { get; private set; }

		public static new OdbSymbolRectangleDonut Create(string param)
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

			if (double.TryParse(split[2], out double lineWidth) is false)
			{
				return null;
			}

			return new OdbSymbolRectangleDonut(width, height, lineWidth);
		}
	}
}
