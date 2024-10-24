namespace CressemExtractLibrary.Data.Interface.Symbol
{
	public interface ISymbolRoundedSquareThermalOpenCorners : ISymbolEditedCorner
	{
		double Diameter { get; }

		double InnerDiameter { get; }

		double Angle { get; }

		int NumberOfSpoke { get; }

		double Gap { get; }
	}
}
