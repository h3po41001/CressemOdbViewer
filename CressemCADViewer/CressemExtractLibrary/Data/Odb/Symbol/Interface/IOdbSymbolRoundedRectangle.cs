namespace CressemExtractLibrary.Data.Odb.Symbol.Interface
{
	public interface IOdbSymbolRoundedRectangle : IOdbSymbolRectangle
	{
		double CornerRadius { get; }

		bool[] IsRounded { get; }

		bool[] ConvertCornerFlag(string corners, int cornerNum);
	}
}
