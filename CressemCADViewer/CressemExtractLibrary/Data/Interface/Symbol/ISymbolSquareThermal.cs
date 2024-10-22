namespace CressemExtractLibrary.Data.Interface.Symbol
{
	public interface ISymbolSquareThermal : ISymbolRound
	{
		double InnerDiameter { get; }

		double Angle { get; }

		int NumberOfSpoke { get; }

		double Gap { get; }
	}
}
