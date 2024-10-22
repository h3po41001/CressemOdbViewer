namespace CressemExtractLibrary.Data.Interface.Symbol
{
	public interface ISymbolRoundedRectangleThermalOpenCorners : ISymbolEditedCorner
	{
		double OuterDiameter { get; }

		double InnerDiameter { get; }

		double Angle { get; }

		int NumberOfSpoke { get; }

		double Gap { get; }
	}
}
