using System.Drawing;
using CressemExtractLibrary.Data.Odb.Symbol.Interface;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolRectangle : OdbSymbolBase, IOdbSymbolRectangle
	{
		protected OdbSymbolRectangle() { }

		public OdbSymbolRectangle(double width, double height) : base()
		{
			Width = width;
			Height = height;
		}

		public double Width { get; private set; }

		public double Height { get; private set; }

		public static OdbSymbolRectangle Create(string param)
		{
			return null;
		}
	}
}
