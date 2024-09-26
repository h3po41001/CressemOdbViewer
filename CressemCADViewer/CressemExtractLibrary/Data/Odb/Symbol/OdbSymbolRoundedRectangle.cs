using System.Drawing;
using CressemExtractLibrary.Data.Odb.Symbol.Interface;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolRoundedRectangle : OdbSymbolEditedCorner, IOdbSymbolRoundedRectangle
	{
		protected OdbSymbolRoundedRectangle() { }

		public OdbSymbolRoundedRectangle(PointF pos,
			double width, double height,
			double corenrRadius, string corners) : base(pos, corenrRadius, corners, 4)
		{
			Width = width;
			Height = height;
		}

		public double Width { get; private set; }

		public double Height { get; private set; }

		public bool[] IsRounded { get => IsEditedCorner; }
	}
}
