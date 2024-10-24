using CressemExtractLibrary.Data.Interface.Symbol;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolSquareDonut : OdbSymbolBase, ISymbolSquareDonut
	{
		protected OdbSymbolSquareDonut() { }

		public OdbSymbolSquareDonut(int index, double outerDiameter, 
			double innerDiameter) : base(index) 
		{
			Diameter = outerDiameter;
			InnerDiameter = innerDiameter;
		}

		public double Diameter { get; private set; }

		public double InnerDiameter { get; private set; }

		public static OdbSymbolSquareDonut Create(int index, string param)
		{
			string[] split = param.Split('X');
			if (split.Length != 2)
			{
				return null;
			}

			if (double.TryParse(split[0], out double outerDiameter) is false)
			{
				return null;
			}

			if (double.TryParse(split[1], out double innerDiameter) is false)
			{
				return null;
			}

			return new OdbSymbolSquareDonut(index, outerDiameter, innerDiameter);
		}
	}
}
