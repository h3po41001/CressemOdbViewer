using System.Drawing;
using CressemExtractLibrary.Data.Odb.Symbol.Interface;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolRectangleDonut : OdbSymbolRectangle, IOdbSymbolRectangleDonut
	{
		protected OdbSymbolRectangleDonut()
		{
		}

		public OdbSymbolRectangleDonut(PointF pos, double width, double height, double lineWidth) : 
			base(pos, width, height)
		{
			LineWidth = lineWidth;
		}

		public double LineWidth { get; private set; }
	}
}
