using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolRectangle : OdbSymbolBase
	{
		protected OdbSymbolRectangle() { }

		public OdbSymbolRectangle(PointF pos, SizeF size) : base(pos)
		{
			Size = size;
		}

		public SizeF Size { get; private set; }
	}
}
