using System.Drawing;
using CressemExtractLibrary.Data.Odb.Symbol.Interface;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolRoundedSqureDonut : OdbSymbolEditedCorner, IOdbSymbolRoundedSqureDonut
	{
		private OdbSymbolRoundedSqureDonut() { }

		public OdbSymbolRoundedSqureDonut(PointF pos, 
			double outerDiameter, double innerDiameter, 
			double cornerRad, string corners) : base(pos, cornerRad, corners, 4)
		{
			Position = pos;
			OuterDiameter = outerDiameter;
			InnerDiameter = innerDiameter;
		}

		public double OuterDiameter { get; private set; }

		public double InnerDiameter { get; private set; }

		public double Radius { get; private set; }

		public bool[] IsRounded { get => IsEditedCorner; }
	}
}
