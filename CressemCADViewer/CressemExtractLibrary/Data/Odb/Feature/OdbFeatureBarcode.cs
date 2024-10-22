using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using CressemExtractLibrary.Data.Interface.Features;

namespace CressemExtractLibrary.Data.Odb.Feature
{
	internal class OdbFeatureBarcode : OdbFeatureBase, IFeatureBarcode
	{
		private OdbFeatureBarcode() { }

		public OdbFeatureBarcode(int index, bool isMM, double x, double y, 
			string name, string font, string polarity,
			double e, double elementWidth, double barcodeHeight, 
			string fasc, string checkSum, string invertBg, 
			string astr, string astrPos, 
			string text, string attrString) : base(index, isMM, x, y, polarity, "", -1, attrString)
		{
			Name = name;
			Font = font;
			E = e;
			ElementWidth = elementWidth;
			BarcodeHeight = barcodeHeight;
			Fasc = fasc;
			CheckSum = checkSum;
			InvertBg = invertBg;
			Astr = astr;
			AstrPos = astrPos;
			Text = text;
		}

		public string Name { get; private set; }

		public string Font { get; private set; }

		// a constant value (reserved for future use)
		public double E { get; private set; }

		public double ElementWidth { get; private set; }

		public double BarcodeHeight { get; private set; }

		// Y for full ASCII, N for partial ASCII
		public string Fasc { get; private set; }

		// Y for checksum, N for no checksum
		public string CheckSum { get; private set; }

		// Y for inverted background, N for no background
		public string InvertBg { get; private set; }

		// Y for an addition of a text string
		public string Astr { get; private set; }

		// T for adding the string on top, B for bottom
		public string AstrPos { get; private set; }

		public string Text { get; private set; }

		public static OdbFeatureBarcode Create(int index, bool isMM, string[] param)
		{
			if (param.Length != 15)
			{
				return null;
			}

			if (param[0] != "B")
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

			if (double.TryParse(param[6], out double e) is false)
			{
				return null;
			}

			if (double.TryParse(param[7], out double elementWidth) is false)
			{
				return null;
			}

			if (double.TryParse(param[8], out double barcodeHeight) is false)
			{
				return null;
			}

			string[] splited = param[10].Split(';');

			string text = splited[0];
			string attrString = splited.Length > 1 ? splited[1] : string.Empty;

			return new OdbFeatureBarcode(index, isMM, x, y, param[3], param[4], param[5],
				e, elementWidth, barcodeHeight,
				param[9], param[10], param[11], param[12], param[13], text, attrString);
		}
	}
}
