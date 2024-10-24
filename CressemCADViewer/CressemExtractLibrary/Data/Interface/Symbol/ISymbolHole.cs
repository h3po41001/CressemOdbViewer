namespace CressemExtractLibrary.Data.Interface.Symbol
{
	public interface ISymbolHole : ISymbolIRegularShape
	{
		string PlatingStatus { get; }

		double PlusTolerance { get; }

		double MinusTolerance { get; }
	}
}
