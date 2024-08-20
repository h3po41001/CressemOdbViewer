using System.Drawing;
using CressemExtractLibrary.Data.Odb.Symbol.Interface;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolRectangle : OdbSymbolBase, IOdbSymbolRectangle
	{
		protected OdbSymbolRectangle() { }

		public OdbSymbolRectangle(PointF pos, double width, double height) : base(pos)
		{
			Width = width;
			Height = height;
		}

		public double Width { get; private set; }

		public double Height { get; private set; }
	}
}
