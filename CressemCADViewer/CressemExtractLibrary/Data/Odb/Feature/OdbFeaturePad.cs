using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CressemExtractLibrary.Data.Odb.Feature
{
	internal class OdbFeaturePad : OdbFeatureBase
	{
		private OdbFeaturePad() { }

		public OdbFeaturePad(int index, bool isMM,
			double x, double y, int symbolNum,
			string polarity, string decode,
			int orientDef) : base(index, isMM, x, y, polarity, decode)
		{
			SymbolNum = symbolNum;
			OrientDef = orientDef;
		}

		public int SymbolNum { get; private set; }

		// 0 : 0도, 1 : 90도, 2 : 180도, 3 : 270도, 4 : 0도 X축 미러, 5 : 90도 X축 미러, 6 : 180도 X축 미러, 7 : 270도 X축 미러
		// 8 :  any angle rotation, no mirror, 9 : any angle rotation, X-axis mirror
		public int OrientDef { get; private set; }

		public static OdbFeaturePad Create(int index, bool isMM, string[] param)
		{
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

			string[] split = param[6].Split(';');

			if (int.TryParse(split[0], out int orientDef) is false)
			{
				return null;
			}

			return new OdbFeaturePad(index, isMM, x, y,
				symbolNum, param[4], param[5], orientDef);
		}
	}
}
