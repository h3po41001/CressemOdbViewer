using System.Drawing;
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
			return null;
		}
	}
}
