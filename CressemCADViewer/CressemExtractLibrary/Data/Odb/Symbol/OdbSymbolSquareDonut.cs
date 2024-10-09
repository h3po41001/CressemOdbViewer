using System.Drawing;
using CressemExtractLibrary.Data.Odb.Symbol.Interface;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolSquareDonut : OdbSymbolSquare, IOdbSymbolSqureDonut
	{
		protected OdbSymbolSquareDonut() { }

		public OdbSymbolSquareDonut(double outerDiameter, 
			double innerDiameter) : base(outerDiameter) 
		{
			InnerDiameter = innerDiameter;
		}

		public double InnerDiameter { get; private set; }

		public static new OdbSymbolSquareDonut Create(string param)
		{
			return null;
		}
	}
}
