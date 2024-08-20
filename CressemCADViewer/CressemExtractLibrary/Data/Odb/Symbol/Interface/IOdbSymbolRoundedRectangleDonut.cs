using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CressemExtractLibrary.Data.Odb.Symbol.Interface
{
	internal interface IOdbSymbolRoundedRectangleDonut : IOdbSymbolRoundedRectangle
	{
		double LineWidth { get; }
	}
}
