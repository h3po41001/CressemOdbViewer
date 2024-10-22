using System.Collections.Generic;
using CressemExtractLibrary.Data.Interface.Features;

namespace CressemExtractLibrary.Data.Interface.Symbol
{
	public interface ISymbolUser : ISymbolBase
	{
		string Name { get; }

		string FeatureFilePath { get; }

		IEnumerable<IFeatureBase> FeaturesList { get; }
	}
}
