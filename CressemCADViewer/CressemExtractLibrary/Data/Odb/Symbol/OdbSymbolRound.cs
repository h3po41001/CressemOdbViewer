using System.Drawing;
using CressemExtractLibrary.Data.Odb.Symbol.Interface;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolRound : OdbSymbolBase, IOdbSymbolRound
	{
		protected OdbSymbolRound()
		{
		}

		public OdbSymbolRound(double diameter) : base()
		{
			Diameter = diameter;
		}

		public double Diameter { get; private set; }

		public static OdbSymbolBase Create(string param)
		{
			if (double.TryParse(param, out double diameter) is false)
			{
				return null;
			}

			return new OdbSymbolRound(diameter);
		}
	}
}
