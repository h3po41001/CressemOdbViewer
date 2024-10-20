namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolRound : OdbSymbolBase
	{
		protected OdbSymbolRound()
		{
		}

		public OdbSymbolRound(int index, double diameter) : base(index)
		{
			Diameter = diameter;
		}

		public double Diameter { get; private set; }

		public static OdbSymbolRound Create(int index, string param)
		{
			if (double.TryParse(param, out double diameter) is false)
			{
				return null;
			}

			return new OdbSymbolRound(index, diameter);
		}
	}
}
