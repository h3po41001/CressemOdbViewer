﻿using System.Collections.Generic;
using CressemExtractLibrary.Data.Odb.Feature;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolUser : OdbSymbolBase
	{
		private OdbSymbolUser()
		{
		}

		public OdbSymbolUser(string name, string path, 
			OdbFeatures features)
		{
			Name = name;
			FeatureFilePath = path;
			Features = features;
		}

		public string Name { get; private set; }

		public string FeatureFilePath { get; private set; }

		public OdbFeatures Features { get; private set; }
	}
}