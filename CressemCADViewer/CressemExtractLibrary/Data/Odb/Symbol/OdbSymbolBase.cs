using System.Drawing;
using CressemExtractLibrary.Data.Odb.Symbol.Interface;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal abstract class OdbSymbolBase : IOdbSymbolBase
	{
		protected OdbSymbolBase() { }

		public PointF Position { get; set; } = new PointF();
	}
}
