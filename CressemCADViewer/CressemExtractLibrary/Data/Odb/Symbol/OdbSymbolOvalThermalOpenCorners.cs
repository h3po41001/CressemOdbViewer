using CressemExtractLibrary.Data.Interface.Symbol;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolOvalThermalOpenCorners : OdbSymbolBase, ISymbolOvalThermalOpenCorners
	{
		private OdbSymbolOvalThermalOpenCorners() { }

		public OdbSymbolOvalThermalOpenCorners(int index, 
			double outerWidth, double outerHeight,
			double angle, int numberOfSpoke, double gap, double lineWidth) : base(index)
		{
			OuterWidth = outerWidth;
			OuterHeight = outerHeight;
			Angle = angle;
			NumberOfSpoke = numberOfSpoke;
			Gap = gap;
			LineWidth = lineWidth;
		}

		public double OuterWidth { get; private set; }

		public double OuterHeight { get; private set; }

		public double Angle { get; private set; }

		public int NumberOfSpoke { get; private set; }

		public double Gap { get; private set; }

		public double LineWidth { get; private set; }

		public static OdbSymbolOvalThermalOpenCorners Create(int index, string param)
		{
			string[] split = param.Split('X');
			if (split.Length != 6)
			{
				return null;
			}

			if (double.TryParse(split[0], out double outerWidth) is false)
			{
				return null;
			}

			if (double.TryParse(split[1], out double outerHeight) is false)
			{
				return null;
			}

			if (double.TryParse(split[2], out double angle) is false)
			{
				return null;
			}

			if (int.TryParse(split[3], out int numberOfSpoke) is false)
			{
				return null;
			}

			if (double.TryParse(split[4], out double gap) is false)
			{
				return null;
			}

			if (double.TryParse(split[5], out double lineWidth) is false)
			{
				return null;
			}

			return new OdbSymbolOvalThermalOpenCorners(index, outerWidth, outerHeight,
				angle, numberOfSpoke, gap, lineWidth);
		}
	}
}