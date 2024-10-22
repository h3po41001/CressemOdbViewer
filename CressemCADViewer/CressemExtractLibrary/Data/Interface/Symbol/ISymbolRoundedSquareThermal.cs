namespace CressemExtractLibrary.Data.Interface.Symbol
{
	public interface ISymbolRoundedSquareThermal : ISymbolEditedCorner, ISymbolRound
	{
		double InnerDiameter { get; }

		double Angle { get; }

		int NumberOfSpoke { get; }

		double Gap { get; }
	}
}
