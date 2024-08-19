using System.Drawing;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolBase
	{
		protected OdbSymbolBase() { }

		public OdbSymbolBase(PointF pos) 
		{
			Pos = pos;
		}

		public PointF Pos { get; private set; }
	}
}
