using System.Drawing;
using CressemExtractLibrary.Data.Odb.Symbol.Interface;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolRoundedRectangleDonut : OdbSymbolRoundedRectangle, IOdbSymbolRoundedRectangleDonut
	{
		private OdbSymbolRoundedRectangleDonut() { }

		public OdbSymbolRoundedRectangleDonut(double width, double height,
			double corenrRadius, string corners, double lineWidth) : 
			base(width, height, corenrRadius, corners) 
		{
			LineWidth = lineWidth;
		}

		public double LineWidth { get; private set; }

		public static new OdbSymbolRoundedRectangleDonut Create(string param)
		{
			return null;
		}
	}
}
