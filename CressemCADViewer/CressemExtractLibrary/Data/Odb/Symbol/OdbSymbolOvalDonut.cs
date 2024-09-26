using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CressemExtractLibrary.Data.Odb.Symbol.Interface;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolOvalDonut : OdbSymbolOval, IOdbSymbolOvalDonut
	{
		private OdbSymbolOvalDonut() { }

		public OdbSymbolOvalDonut(PointF pos,
			double width, double height, double lineWidth) : base(pos, width, height)
		{
			LineWidth = lineWidth;
		}

		public double LineWidth { get; private set; }
	}
}
