namespace CressemExtractLibrary.Data.Interface.Symbol
{
	public interface ISymbolSquareRoundThermal : ISymbolRound
	{
		double InnerDiameter { get; }

		double Angle { get; }

		int NumberOfSpoke { get; }

		double Gap { get; }
	}
}
