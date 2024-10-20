using CressemExtractLibrary.Data.Interface.Features;

namespace CressemExtractLibrary.Data.Odb.Feature
{
	internal class OdbFeatureText : OdbFeatureBase, IFeatureText
	{
		private OdbFeatureText() { }

		public OdbFeatureText(int index, bool isMM, double x, double y,
			string font, string polarity, int orientDef,
			double sizeX, double sizeY, double widthFactor,
			string text, int version) : base(index, isMM, x, y, polarity, "")
		{
			Font = font;
			OrientDef = orientDef;
			SizeX = sizeX;
			SizeY = sizeY;
			WidthFactor = widthFactor;
			Text = text;
			Version = version;
		}

		public string Font { get; private set; }

		// 0 : 0도, 1 : 90도, 2 : 180도, 3 : 270도, 4 : 0도 X축 미러, 5 : 90도 X축 미러, 6 : 180도 X축 미러, 7 : 270도 X축 미러
		// 8 :  any angle rotation, no mirror, 9 : any angle rotation, X-axis mirror
		public int OrientDef { get; private set; }

		public double SizeX { get; private set; }

		public double SizeY { get; private set; }

		public double WidthFactor { get; private set; }

		public string Text { get; private set; }

		// 0 : previous version, 1 : current version
		public int Version { get; private set; }

		public static OdbFeatureText Create(int index, bool isMM, string[] param)
		{
			if (param.Length != 11)
			{
				return null;
			}

			if (param[0] != "T")
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

			if (int.TryParse(param[5], out int orientDef) is false)
			{
				return null;
			}

			if (double.TryParse(param[6], out double sizeX) is false)
			{
				return null;
			}

			if (double.TryParse(param[7], out double sizeY) is false)
			{
				return null;
			}

			if (double.TryParse(param[8], out double widthFactor) is false)
			{
				return null;
			}

			string[] split = param[10].Split(';');

			if (int.TryParse(split[0], out int version) is false)
			{
				return null;
			}

			return new OdbFeatureText(index, isMM, x, y, param[3], param[4],
				orientDef, sizeX, sizeY, widthFactor, param[9], version);
		}
	}
}
