using System.Collections.Generic;
using CressemExtractLibrary.Data.Odb.Feature;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolUser : OdbSymbolBase
	{
		private OdbSymbolUser()
		{
		}

		public OdbSymbolUser(string name, string path,
			List<OdbFeatures> features)
		{
			Name = name;
			FeatureFilePath = path;
			Features = new List<OdbFeatures>(features);
		}

		public string Name { get; private set; }

		public string FeatureFilePath { get; private set; }

		public List<OdbFeatures> Features { get; private set; }
	}
}
