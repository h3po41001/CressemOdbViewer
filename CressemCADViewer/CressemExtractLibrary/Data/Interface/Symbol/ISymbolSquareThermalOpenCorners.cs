namespace CressemExtractLibrary.Data.Interface.Symbol
{
	public interface ISymbolSquareThermalOpenCorners : ISymbolRound
	{
		double InnerDiameter { get; }

		double Angle { get; }

		int NumberOfSpoke { get; }

		double Gap { get; }
	}
}
