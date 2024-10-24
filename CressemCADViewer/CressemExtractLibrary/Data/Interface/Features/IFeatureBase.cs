using CressemExtractLibrary.Data.Interface.Symbol;

namespace CressemExtractLibrary.Data.Interface.Features
{
	public interface IFeatureBase
	{
		bool IsMM { get; }

		double X { get; }

		double Y { get; }

		string Polarity { get; }

		int Orient { get; }

		bool IsMirrorXAxis { get; }

		ISymbolBase FeatureSymbol { get; }
	}
}
