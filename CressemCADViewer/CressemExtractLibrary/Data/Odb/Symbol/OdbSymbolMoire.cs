using CressemExtractLibrary.Data.Interface.Symbol;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolMoire : OdbSymbolBase, ISymbolMoire
	{
		private OdbSymbolMoire() { }

		public OdbSymbolMoire(int index,
			double ringWidth, double ringHeight, int numberOfRing,
			double lineWidth, double lineLength, double lineAngle) : base(index)
		{
			RingWidth = ringWidth;
			RingHeight = ringHeight;
			NumberOfRing = numberOfRing;
			LineWidth = lineWidth;
			LineLength = lineLength;
			LineAngle = lineAngle;
		}

		public double RingWidth { get; private set; }

		public double RingHeight { get; private set; }

		public int NumberOfRing { get; private set; }

		public double LineWidth { get; private set; }

		public double LineLength { get; private set; }

		public double LineAngle { get; private set; }


		public static OdbSymbolMoire Create(int index, string param)
		{
			string[] split = param.Split('X');
			if (split.Length != 6)
			{
				return null;
			}

			if (double.TryParse(split[0], out double ringWidth) is false)
			{
				return null;
			}

			if (double.TryParse(split[1], out double ringHeight) is false)
			{
				return null;
			}

			if (int.TryParse(split[2], out int numberOfRing) is false)
			{
				return null;
			}

			if (double.TryParse(split[3], out double lineWidth) is false)
			{
				return null;
			}

			if (double.TryParse(split[4], out double lineLength) is false)
			{
				return null;
			}

			if (double.TryParse(split[5], out double lineAngle) is false)
			{
				return null;
			}

			return new OdbSymbolMoire(index, ringWidth, ringHeight,
				numberOfRing, lineWidth, lineLength, lineAngle);
		}
	}
}
