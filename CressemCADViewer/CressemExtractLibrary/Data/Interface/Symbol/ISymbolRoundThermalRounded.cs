namespace CressemExtractLibrary.Data.Interface.Symbol
{
	public interface ISymbolRoundThermalRounded : ISymbolRound
	{
		double InnerDiameter { get; }

		double Angle { get; }

		int NumberOfSpoke { get; }

		double Gap { get; }
	}
}
