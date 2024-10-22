namespace CressemExtractLibrary.Data.Interface.Symbol
{
	public interface ISymbolHole : ISymbolBase
	{
		double Diameter { get; }

		string PlatingStatus { get; }

		double PlusTolerance { get; }

		double MinusTolerance { get; }
	}
}
