using CressemExtractLibrary.Data.Interface.Features;

namespace CressemExtractLibrary.Data.Odb.Feature
{
	internal class OdbFeaturePad : OdbFeatureBase, IFeaturePad
	{
		private OdbFeaturePad() { }

		public OdbFeaturePad(int index, bool isMM,
			double x, double y, int symbolNum,
			string polarity, string decode,	int orientDef,
			string attrString) : base(index, isMM, x, y, polarity, decode, orientDef, symbolNum, attrString)
		{
		}

		public static OdbFeaturePad Create(int index, bool isMM, string paramString)
		{
			string[] splited = paramString.ToUpper().Split(';');
			string[] param = splited[0].Trim().Split(' ');
			string attrString = splited.Length > 1 ? splited[1] : string.Empty;

			if (param.Length != 7)
			{
				return null;
			}

			if (param[0] != "P")
			{
				return null;
			}

			if (double.TryParse(param[1], out double x) is false)
			{
				return null;
			}

			if (double.TryParse(param[2], out double y) is false)
			{
				return null;
			}

			if (int.TryParse(param[3], out int symbolNum) is false)
			{
				return null;
			}

			if (int.TryParse(param[6], out int orientDef) is false)
			{
				return null;
			}

			return new OdbFeaturePad(index, isMM, x, y,
				symbolNum, param[4], param[5], orientDef, attrString);
		}
	}
}
