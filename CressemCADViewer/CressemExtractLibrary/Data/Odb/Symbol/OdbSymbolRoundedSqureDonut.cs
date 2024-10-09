using System.Drawing;
using CressemExtractLibrary.Data.Odb.Symbol.Interface;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolRoundedSqureDonut : OdbSymbolEditedCorner, IOdbSymbolRoundedSqureDonut
	{
		private OdbSymbolRoundedSqureDonut() { }

		public OdbSymbolRoundedSqureDonut(double outerDiameter, 
			double innerDiameter, double cornerRad, 
			string corners) : base(cornerRad, corners, 4)
		{
			OuterDiameter = outerDiameter;
			InnerDiameter = innerDiameter;
		}

		public double OuterDiameter { get; private set; }

		public double InnerDiameter { get; private set; }

		public double Radius { get; private set; }

		public bool[] IsRounded { get => IsEditedCorner; }

		public static new OdbSymbolRoundedSqureDonut Create(string param)
		{
			return null;
		}
	}
}
