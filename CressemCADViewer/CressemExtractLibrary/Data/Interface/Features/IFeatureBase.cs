using CressemExtractLibrary.Data.Interface.Symbol;

namespace CressemExtractLibrary.Data.Interface.Features
{
	public interface IFeatureBase
	{
		int Index { get; }

		bool IsMM { get; }

		double X { get; }

		double Y { get; }

		string Polarity { get; }

		int Orient { get; }

		bool IsFlipHorizontal { get; }

		ISymbolBase FeatureSymbol { get; }
	}
}
