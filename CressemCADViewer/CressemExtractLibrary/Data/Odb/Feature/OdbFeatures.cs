using System.Collections.Generic;
using CressemExtractLibrary.Data.Odb.Attribute;
using CressemExtractLibrary.Data.Odb.Symbol;

namespace CressemExtractLibrary.Data.Odb.Feature
{
	internal class OdbFeatures
	{
		public OdbFeatures()
		{
			Symbols = new List<OdbSymbolBase>();
			FeatureList = new List<OdbFeatureBase>();
		}

		public OdbAttributeList AttributeList { get; private set; }

		public List<OdbSymbolBase> Symbols { get; private set; }

		public List<OdbFeatureBase> FeatureList { get; private set; }

		public void AddSymbol(OdbSymbolBase symbol)
		{
			Symbols.Add(symbol);
		}

		public void AddFeature(OdbFeatureBase feature)
		{
			FeatureList.Add(feature);
		}
	}
}
