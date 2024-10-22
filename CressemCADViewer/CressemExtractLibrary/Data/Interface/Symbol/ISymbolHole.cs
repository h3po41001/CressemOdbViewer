namespace CressemExtractLibrary.Data.Interface.Symbol
{
	public interface ISymbolHole : ISymbolRound
	{
		string PlatingStatus { get; }

		double PlusTolerance { get; }

		double MinusTolerance { get; }
	}
}
