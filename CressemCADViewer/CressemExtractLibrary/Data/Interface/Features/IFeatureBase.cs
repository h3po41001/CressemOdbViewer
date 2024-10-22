using CressemExtractLibrary.Data.Interface.Symbol;

namespace CressemExtractLibrary.Data.Interface.Features
{
	public interface IFeatureBase
	{
		bool IsMM { get; }

		double X { get; }

		double Y { get; }

		string Polarity { get; }

		ISymbolBase FeatureSymbol { get; }
	}
}
