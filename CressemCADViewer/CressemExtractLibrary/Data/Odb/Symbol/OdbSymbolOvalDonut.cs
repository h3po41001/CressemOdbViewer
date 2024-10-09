using System.Drawing;
using CressemExtractLibrary.Data.Odb.Symbol.Interface;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolOvalDonut : OdbSymbolOval, IOdbSymbolOvalDonut
	{
		private OdbSymbolOvalDonut() { }

		public OdbSymbolOvalDonut(double width, double height, 
			double lineWidth) : base(width, height)
		{
			LineWidth = lineWidth;
		}

		public double LineWidth { get; private set; }

		public static new OdbSymbolOvalDonut Create(string param)
		{
			return null;
		}
	}
}
