namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal abstract class OdbSymbolBase
	{
		protected OdbSymbolBase() { }

		protected OdbSymbolBase(int index)
		{
			Index = index;
		}

		public int Index { get; private set; } = -1;
	}
}
