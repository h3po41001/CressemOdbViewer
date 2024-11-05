using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CressemExtractLibrary.Data.Interface.Features;
using CressemExtractLibrary.Data.Interface.Font;
using CressemExtractLibrary.Data.Odb.Font;
using CressemExtractLibrary.Data.Odb.Loader;

namespace CressemExtractLibrary.Data.Odb.Feature
{
	internal class OdbFeatureText : OdbFeatureBase, IFeatureText
	{
		private OdbFeatureText() { }

		public OdbFeatureText(int index, bool isMM, double x, double y,
			string font, string polarity, int orientDef,
			double sizeX, double sizeY, double widthFactor,
			string text, int version,
			string attrString) : base(index, isMM, x, y, polarity, "", orientDef, -1, attrString)
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

		// width of character segment (in units of 12 mils) i.e. 1 = 12
		// mils, 0.5 = 6 mils
		public double WidthFactor { get; private set; }

		public string Text { get; private set; }

		// 0 : previous version, 1 : current version
		public int Version { get; private set; }

		public IFont FeatureFont { get; private set; }

		public static OdbFeatureText Create(int index, bool isMM, string layerName,
			string paramString)
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

			var text = param[9].Replace("'", "");
			if (text.ToUpper().Equals("$$DATE") is true)
			{
				CultureInfo usCulture = new CultureInfo("en-US");
				text = DateTime.Today.ToString("MM/dd/yy", usCulture);
			}
			else if (text.ToUpper().Equals("$$JOB") is true)
			{
				text = OdbInfos.Instance.JobName.ToUpper();
			}
			else if (text.ToUpper().Equals("$$LAYER") is true)
			{
				text = layerName.ToUpper();
			}

			if (int.TryParse(param[10], out int version) is false)
			{
				return null;
			}

			return new OdbFeatureText(index, isMM, x, y, param[3], param[4],
				orientDef, sizeX, sizeY, widthFactor, text, version, attrString);
		}

		public void SetFonts(List<OdbFont> fonts)
		{
			if (fonts is null || fonts.Any() is false)
			{
				return;
			}

			FeatureFont = fonts.FirstOrDefault(x => x.Name.ToUpper().Equals(Font));
		}
	}
}
