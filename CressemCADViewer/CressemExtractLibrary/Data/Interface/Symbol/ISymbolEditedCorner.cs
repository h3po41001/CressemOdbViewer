namespace CressemExtractLibrary.Data.Interface.Symbol
{
	public interface ISymbolEditedCorner : ISymbolBase
	{
		double CornerRadius { get; }

		bool[] IsEditedCorner { get; }
	}
}
