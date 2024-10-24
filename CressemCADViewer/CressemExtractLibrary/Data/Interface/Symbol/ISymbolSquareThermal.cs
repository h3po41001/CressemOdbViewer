namespace CressemExtractLibrary.Data.Interface.Symbol
{
	public interface ISymbolSquareThermal : ISymbolBase
	{
		double OuterDiameter { get; }

		double InnerDiameter { get; }

		double Angle { get; }

		int NumberOfSpoke { get; }

		double Gap { get; }
	}
}
