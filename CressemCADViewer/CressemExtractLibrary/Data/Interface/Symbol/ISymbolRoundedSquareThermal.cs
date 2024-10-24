namespace CressemExtractLibrary.Data.Interface.Symbol
{
	public interface ISymbolRoundedSquareThermal : ISymbolEditedCorner, ISymbolIRegularShape
	{
		double InnerDiameter { get; }

		double Angle { get; }

		int NumberOfSpoke { get; }

		double Gap { get; }
	}
}
