using CressemExtractLibrary.Data.Interface.Features;

namespace CressemExtractLibrary.Data.Odb.Feature
{
	internal class OdbFeatureText : OdbFeatureBase, IFeatureText
	{
		private OdbFeatureText() { }

		public OdbFeatureText(int index, bool isMM, double x, double y,
			string font, string polarity, int orientDef,
			double sizeX, double sizeY, double widthFactor,
			string text, int version, 
			string attrString) : base(index, isMM, x, y, polarity, "", orientDef, - 1, attrString)
		{
			Font = font;
			SizeX = sizeX;
			SizeY = sizeY;
			WidthFactor = widthFactor;
			Text = text;
			Version = version;
		}

		public string Font { get; private set; }

		public double SizeX { get; private set; }

		public double SizeY { get; private set; }

		public double WidthFactor { get; private set; }

		public string Text { get; private set; }

		// 0 : previous version, 1 : current version
		public int Version { get; private set; }

		public static OdbFeatureText Create(int index, bool isMM, string paramString)
		{
			string[] splited = paramString.ToUpper().Split(';');
			string[] param = splited[0].Trim().Split(' ');
			string attrString = splited.Length > 1 ? splited[1] : string.Empty;

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

			if (int.TryParse(param[10], out int version) is false)
			{
				return null;
			}

			return new OdbFeatureText(index, isMM, x, y, param[3], param[4],
				orientDef, sizeX, sizeY, widthFactor, param[9], version, attrString);
		}
	}
}
