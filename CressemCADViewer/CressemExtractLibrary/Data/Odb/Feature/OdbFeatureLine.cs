using CressemExtractLibrary.Data.Interface.Features;

namespace CressemExtractLibrary.Data.Odb.Feature
{
	internal class OdbFeatureLine : OdbFeatureBase, IFeatureLine
	{
		private OdbFeatureLine() { }

		public OdbFeatureLine(int index, bool isMM,
			double sx, double sy, double ex, double ey,
			int symbolNum, string polarity, string decode,
			string attrString) : base(index, isMM, sx, sy, polarity, decode, 0, symbolNum, attrString)
		{
			Ex = ex;
			Ey = ey;
		}

		public double Ex { get; private set; }

		public double Ey { get; private set; }

		static public OdbFeatureLine Create(int index, bool isMM, string[] param)
		{
			if (param.Length != 9)
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

			string[] splited = param[7].Split(';');

			string decode = splited[0];
			string attrString = splited.Length > 1 ? splited[1] : string.Empty;

			return new OdbFeatureLine(index, isMM, sx, sy, ex, ey, symbolNum, param[6], decode, attrString);
		}
	}
}
