using System.Collections.Generic;

namespace CressemExtractLibrary.Data.Odb.Font
{
	internal class OdbFont
	{
		private OdbFont() { }

		public OdbFont(string name, string path)
		{
			Name = name;
			Path = path;
			FontAttrs = new List<OdbFontAttr>();
		}

		public string Name { get; private set; }

		public string Path { get; private set; }

		public List<OdbFontAttr> FontAttrs { get; private set; }

		public void AddFontAttr(OdbFontAttr fontAttr)
		{
			FontAttrs.Add(fontAttr);
		}
	}

	internal class OdbFontAttr
	{
		private OdbFontAttr() { }

		public OdbFontAttr(string character, bool isMM)
		{
			Character = character;
			IsMM = isMM;
		}

		public string Character { get; private set; }

		public bool IsMM { get; private set; }

		public List<OdbFontLine> Lines { get; private set; } = new List<OdbFontLine>();

		public void AddLine(OdbFontLine line)
		{
			Lines.Add(line);
		}
	}

	internal class OdbFontLine
	{
		private OdbFontLine() { }

		public OdbFontLine(double sx, double sy, double ex, double ey,
			string polarity, string shape, double width)
		{
			SX = sx;
			SY = sy;
			EX = ex;
			EY = ey;
			Polarity = polarity;
			Shape = shape;
		}

		public double SX { get; private set; }

		public double SY { get; private set; }

		public double EX { get; private set; }

		public double EY { get; private set; }

		public string Polarity { get; private set; }

		public string Shape { get; private set; }
	}
}
