namespace CressemExtractLibrary.Data.Interface.Symbol
{
	public interface ISymbolRectangularThermalOpenCorners : ISymbolBase
	{
		double OuterWidth { get; }

		double OuterHeight { get; }

		double Angle { get; }

		int NumberOfSpoke { get; }

		double Gap { get; }
	}
}
