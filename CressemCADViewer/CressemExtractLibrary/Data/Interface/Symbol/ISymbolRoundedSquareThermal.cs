namespace CressemExtractLibrary.Data.Interface.Symbol
{
	public interface ISymbolRoundedSquareThermal : ISymbolEditedCorner
	{
		double OuterDiameter { get; }

		double InnerDiameter { get; }

		double Angle { get; }

		int NumberOfSpoke { get; }

		double Gap { get; }
	}
}
