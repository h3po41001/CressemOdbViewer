using System.Drawing;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolOval : OdbSymbolBase
	{
		protected OdbSymbolOval() { }

		public OdbSymbolOval(PointF pos, SizeF size) : base(pos)
		{
			Size = size;
		}

		public SizeF Size { get; private set; }
	}
}
