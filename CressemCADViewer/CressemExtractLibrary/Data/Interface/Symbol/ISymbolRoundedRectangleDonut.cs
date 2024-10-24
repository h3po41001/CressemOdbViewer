namespace CressemExtractLibrary.Data.Interface.Symbol
{
	public interface ISymbolRoundedRectangleDonut : ISymbolEditedCorner
	{
		double Width { get; }

		double Height { get; }

		double LineWidth { get; }
	}
}
