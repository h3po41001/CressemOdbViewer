namespace CressemExtractLibrary.Data.Interface.Symbol
{
	public interface ISymbolChamferedRectangle : ISymbolEditedCorner
	{
		double Width { get; }

		double Height { get; }

		// RT = 0, LT = 1, LB = 2, RB = 3
		bool[] IsChamfered { get; }
	}
}
