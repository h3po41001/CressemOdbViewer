
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolChamferedRectangle : OdbSymbolRectangle
	{
		private OdbSymbolChamferedRectangle() { }

		public OdbSymbolChamferedRectangle(PointF pos, SizeF size,
			double rad, string corners) : base(pos, size)
		{
			Radius = rad;

			var flags = corners.ToCharArray()?.Select(x => x.ToString());

			foreach (var corner in flags)
			{
				int index = int.Parse(corner);
				IsChamfered[index] = true;
			}
		}

		public double Radius { get; private set; }

		// RT = 0, LT = 1, LB = 2, RB = 3
		public bool[] IsChamfered { get; private set; } = new bool[4];
	}
}
