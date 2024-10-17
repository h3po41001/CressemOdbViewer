namespace CressemExtractLibrary.Data.Odb.Feature
{
	internal class OdbFeatureLine : OdbFeatureBase
	{
		private OdbFeatureLine() { }

		public OdbFeatureLine(bool isMM, double sx, double sy, double ex, double ey, int symbolNum, 
			string polarity, string decode) : base(isMM, polarity, decode)
		{
			Sx = sx;
			Sy = sy;
			Ex = ex;
			Ey = ey;
			SymbolNum = symbolNum;
		}

		public double Sx { get; private set; }

		public double Sy { get; private set; }

		public double Ex { get; private set; }

		public double Ey { get; private set; }

		public int SymbolNum { get; private set; }

		static public OdbFeatureLine Create(bool isMM, string[] param)
		{
			if (param.Length != 8)
			{
				return null;
			}

			if (param[0] != "L")
			{
				return null;
			}

			if (double.TryParse(param[1], out double sx) is false)
			{
				return null;
			}

			if (double.TryParse(param[2], out double sy) is false)
			{
				return null;
			}

			if (double.TryParse(param[3], out double ex) is false)
			{
				return null;
			}

			if (double.TryParse(param[4], out double ey) is false)
			{
				return null;
			}

			if (int.TryParse(param[5], out int symbolNum) is false)
			{
				return null;
			}

			return new OdbFeatureLine(isMM, sx, sy, ex, ey, symbolNum, param[6], param[7]);
		}
	}
}
