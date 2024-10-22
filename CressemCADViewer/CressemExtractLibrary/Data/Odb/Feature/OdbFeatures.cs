using System.Collections.Generic;
using CressemExtractLibrary.Data.Odb.Attribute;
using CressemExtractLibrary.Data.Odb.Symbol;

namespace CressemExtractLibrary.Data.Odb.Feature
{
	internal class OdbFeatures
	{
		public OdbFeatures()
		{
			AttrNames = new List<OdbAttributeName>();
			AttrTexts = new List<OdbAttributeTextString>();
			Symbols = new List<OdbSymbolBase>();
			FeatureList = new List<OdbFeatureBase>();
		}

		public List<OdbAttributeName> AttrNames { get; private set; }

		public List<OdbAttributeTextString> AttrTexts { get; private set; }

		public List<OdbSymbolBase> Symbols { get; private set; }

		public List<OdbFeatureBase> FeatureList { get; private set; }

		public void AddAttributeText(OdbAttributeTextString attrText)
		{
			AttrTexts.Add(attrText);
		}

		public void AddAttributeName(OdbAttributeName attrName)
		{
			AttrNames.Add(attrName);
		}

		public void AddSymbol(OdbSymbolBase symbol)
		{
			Symbols.Add(symbol);
		}

		public void AddFeature(OdbFeatureBase feature)
		{
			feature.SetSymbol(Symbols);
			feature.SetAttribute(AttrNames, AttrTexts);
			FeatureList.Add(feature);
		}
	}
}
