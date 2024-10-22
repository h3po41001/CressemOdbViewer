namespace CressemExtractLibrary.Data.Interface.Symbol
{
	public interface ISymbolRoundThermalRounded : ISymbolBase
	{
		double OuterDiameter { get; }

		double InnerDiameter { get; }

		double Angle { get; }

		int NumberOfSpoke { get; }

		double Gap { get; }
	}
}
