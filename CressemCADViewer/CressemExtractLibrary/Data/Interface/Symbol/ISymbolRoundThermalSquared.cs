namespace CressemExtractLibrary.Data.Interface.Symbol
{
	public interface ISymbolRoundThermalSquared : ISymbolRound
	{
		double InnerDiameter { get; }

		double Angle { get; }

		int NumberOfSpoke { get; }

		double Gap { get; }
	}
}
