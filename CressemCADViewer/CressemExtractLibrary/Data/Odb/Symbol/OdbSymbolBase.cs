using CressemExtractLibrary.Data.Interface.Symbol;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal abstract class OdbSymbolBase : ISymbolBase
	{
		protected OdbSymbolBase() { }

		protected OdbSymbolBase(int index)
		{
			Index = index;
		}

		public int Index { get; private set; } = -1;

		public int Orient { get; set; } = 0;
	}
}
