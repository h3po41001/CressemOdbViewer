﻿using System.Drawing;
using CressemExtractLibrary.Data.Odb.Symbol.Interface;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolOval : OdbSymbolRectangle, IOdbSymbolOval
	{
		protected OdbSymbolOval() { }

		public OdbSymbolOval(PointF pos,
			double width, double height) : base(pos, width, height)
		{
		}
	}
}