using System.Collections.Generic;
using CressemExtractLibrary.Data.Interface.Features;
using CressemExtractLibrary.Data.Interface.Symbol;
using CressemExtractLibrary.Data.Odb.Feature;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolUser : OdbSymbolBase, ISymbolUser
	{
		private OdbSymbolUser()
		{
		}

		public OdbSymbolUser(int index, string name, string path, 
			OdbFeatures features) : base(index)
		{
			Name = name;
			FeatureFilePath = path;
			Features = features;
		}

		public string Name { get; private set; }

		public string FeatureFilePath { get; private set; }

		public OdbFeatures Features { get; private set; }

		public IEnumerable<IFeatureBase> FeaturesList { get => Features.FeatureList; }
	}
}
