using System.Drawing;
using CressemExtractLibrary.Data.Odb.Symbol.Interface;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal abstract class OdbSymbolBase : IOdbSymbolBase
	{
		protected OdbSymbolBase() { }

		protected OdbSymbolBase(int index)
		{
			Index = index;
		}

		public int Index { get; private set; } = -1;
	}
}
