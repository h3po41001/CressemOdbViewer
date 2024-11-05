using System.Collections.Generic;
using CressemExtractLibrary.Data.Interface.Font;

namespace CressemExtractLibrary.Data.Odb.Font
{
	internal class OdbFont : IFont
	{
		private List<OdbFontAttr> _fontAttrs;

		private OdbFont() { }

		public OdbFont(string name, string path)
		{
			Name = name;
			Path = path;
			_fontAttrs = new List<OdbFontAttr>();
		}

		public string Name { get; private set; }

		public string Path { get; private set; }

		public double XSize { get; set; }

		public double YSize { get; set; }

		public double Offset { get; set; }

		public IEnumerable<IFontAttribute> FontAttrs { get => _fontAttrs; }

		public void AddFontAttr(OdbFontAttr fontAttr)
		{
			_fontAttrs.Add(fontAttr);
		}
	}

	internal class OdbFontAttr : IFontAttribute
	{
		private List<OdbFontLine> _fontLines = null;

		private OdbFontAttr() { }

		public OdbFontAttr(string character, bool isMM)
		{
			Character = character;
			IsMM = isMM;
			_fontLines = new List<OdbFontLine>();
		}

		public string Character { get; private set; }

		public bool IsMM { get; private set; }

		public IEnumerable<IFontLine> FontLines { get => _fontLines; }

		public void AddLine(OdbFontLine line)
		{
			_fontLines.Add(line);
		}
	}

	internal class OdbFontLine : IFontLine
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
			Width = width;
		}

		public double SX { get; private set; }

		public double SY { get; private set; }

		public double EX { get; private set; }

		public double EY { get; private set; }

		public string Polarity { get; private set; }

		public string Shape { get; private set; }

		public double Width { get; private set; }
	}
}
