using System.Collections.Generic;
using CressemExtractLibrary.Data.Interface.Features;
using CressemExtractLibrary.Data.Interface.Symbol;
using CressemExtractLibrary.Data.Odb.Attribute;
using CressemExtractLibrary.Data.Odb.Symbol;

namespace CressemExtractLibrary.Data.Odb.Feature
{
	internal class OdbFeatureBase : IFeatureBase
	{
		protected OdbFeatureBase() { }

		public OdbFeatureBase(int index, bool isMM, double x, double y,
			string polarity, string decode, int orient, int symbolNum,
			string attributeString)
		{
			Index = index;
			IsMM = isMM;
			X = x;
			Y = y;
			Polarity = polarity;
			Decode = decode;
			OrientDef = orient;
			SymbolNum = symbolNum;
			AttributeString = attributeString;
		}

		public int Index { get; private set; }

		public bool IsMM { get; private set; }

		public double X { get; private set; }

		public double Y { get; private set; }

		public string Polarity { get; private set; }

		// gerber decode
		public string Decode { get; private set; }

		// 0 : 0도, 1 : 90도, 2 : 180도, 3 : 270도, 4 : 0도 X축 미러, 5 : 90도 X축 미러, 6 : 180도 X축 미러, 7 : 270도 X축 미러
		// 8 :  any angle rotation, no mirror, 9 : any angle rotation, X-axis mirror
		public int OrientDef { get; private set; }

		public int Orient { get => (OrientDef % 4) * 90; }

		public bool IsMirrorXAxis { get => OrientDef >= 4; }

		public int SymbolNum { get; private set; }

		public string AttributeString { get; private set; }

		public ISymbolBase FeatureSymbol { get; private set; }

		public List<OdbAttribute> FeatureAttributes { get; private set; }

		public void SetSymbol(List<OdbSymbolBase> symbols)
		{
			if (SymbolNum < 0 || SymbolNum >= symbols.Count)
			{
				return;
			}

			FeatureSymbol = symbols[SymbolNum];
		}

		public void SetAttribute(List<OdbAttributeName> attrNames, List<OdbAttributeTextString> attrTexts)
		{
			if (AttributeString == string.Empty)
			{
				return;
			}

			FeatureAttributes = new List<OdbAttribute>();

			string[] splited = AttributeString.Split(',');
			foreach (var split in splited)
			{
				string[] attrSplited = split.Split('=');
				if (int.TryParse(attrSplited[0], out int index) is false)
				{
					continue;
				}

				if (attrSplited.Length == 1)
				{
					FeatureAttributes.Add(new OdbAttribute(attrNames[index].Name, string.Empty));
				}
				else if (attrSplited.Length == 2)
				{
					if (int.TryParse(attrSplited[1], out int textIndex) is true)
					{
						FeatureAttributes.Add(new OdbAttribute(attrNames[index].Name, attrTexts[textIndex].Text));
					}
				}
			}
		}
	}
}
