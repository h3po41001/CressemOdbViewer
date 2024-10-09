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
			return null;
		}
	}
}
