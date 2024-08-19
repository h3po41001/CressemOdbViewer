using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolRoundedRectangle : OdbSymbolRectangle
	{
		private OdbSymbolRoundedRectangle() { }

		public OdbSymbolRoundedRectangle(PointF pos, SizeF size, 
			double rad, string corners) : base(pos, size)
		{
			Radius = rad;
			
			var flags = corners.ToCharArray()?.Select(x => x.ToString());

			foreach (var corner in flags)
			{
				int index = int.Parse(corner);
				IsRounded[index] = true;
			}
		}

		public double Radius { get; private set; }

		public bool[] IsRounded { get; private set; } = new bool[4];
	}
}
