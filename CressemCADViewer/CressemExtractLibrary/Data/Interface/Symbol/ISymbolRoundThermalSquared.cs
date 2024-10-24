namespace CressemExtractLibrary.Data.Interface.Symbol
{
	public interface ISymbolRoundThermalSquared : ISymbolBase
	{
		double Diameter { get; }

		double InnerDiameter { get; }

		double Angle { get; }

		int NumberOfSpoke { get; }

		double Gap { get; }
	}
}
