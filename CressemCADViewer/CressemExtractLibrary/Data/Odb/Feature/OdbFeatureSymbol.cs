using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CressemExtractLibrary.Data.Odb.Symbol;

namespace CressemExtractLibrary.Data.Odb.Feature
{
	internal class OdbFeatureSymbol : OdbFeatures
	{
		private OdbFeatureSymbol()
		{
		}

		public OdbFeatureSymbol(string name, string param, OdbSymbolBase odbSymbol,
			bool isMM) : base(isMM)
		{
			Name = name;
			Param = param;
			OdbSymbol = odbSymbol;
		}

		public string Name { get; private set; }

		public string Param { get; private set; }

		public OdbSymbolBase OdbSymbol { get; private set; }
	}
}
