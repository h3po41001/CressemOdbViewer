namespace CressemExtractLibrary.Data.Interface.Symbol
{
	public interface ISymbolRoundedSquareThermalOpenCorners : ISymbolEditedCorner, ISymbolRound
	{
		double InnerDiameter { get; }

		double Angle { get; }

		int NumberOfSpoke { get; }

		double Gap { get; }
	}
}
