namespace CressemExtractLibrary.Data.Interface.Symbol
{
	public interface ISymbolRoundedRectangle : ISymbolEditedCorner
	{
		double Width { get; }

		double Height { get; }
	}
}
