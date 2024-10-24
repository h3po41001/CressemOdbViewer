namespace CressemExtractLibrary.Data.Interface.Symbol
{
	public interface ISymbolOctagon : ISymbolBase
	{
		double Width { get; }

		double Height { get; }

		double CornerSize { get; }
	}
}
