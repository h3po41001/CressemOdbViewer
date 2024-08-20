using System.Drawing;
using CressemExtractLibrary.Data.Odb.Symbol.Interface;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolBase : IOdbSymbolBase
	{
		protected OdbSymbolBase() { }

		protected OdbSymbolBase(PointF pos)
		{
			Position = pos;
		}

		public PointF Position { get; set; } = new PointF();
	}
}
