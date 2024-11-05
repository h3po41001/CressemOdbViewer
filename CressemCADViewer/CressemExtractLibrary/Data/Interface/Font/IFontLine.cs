namespace CressemExtractLibrary.Data.Interface.Font
{
	public interface IFontLine
	{
		double SX { get; }

		double SY { get; }

		double EX { get; }

		double EY { get; }

		string Polarity { get; }

		string Shape { get; }

		double Width { get; }
	}
}
