namespace CressemExtractLibrary.Data.Odb.Symbol.Interface
{
	public interface IOdbSymbolChamferedRectangle : IOdbSymbolRectangle
	{
		double CornerRadius { get; }

		bool[] IsChamfered { get; }

		bool[] ConvertCornerFlag(string corners, int cornerNum);
	}
}
