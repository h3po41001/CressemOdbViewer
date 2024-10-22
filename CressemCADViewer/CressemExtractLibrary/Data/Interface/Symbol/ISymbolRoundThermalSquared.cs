namespace CressemExtractLibrary.Data.Interface.Symbol
{
	public interface ISymbolRoundThermalSquared : ISymbolBase
	{
		double OuterDiameter { get; }

		double InnerDiameter { get; }

		double Angle { get; }

		int NumberOfSpoke { get; }

		double Gap { get; }
	}
}
