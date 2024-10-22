using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CressemExtractLibrary.Data.Interface.Symbol
{
	public interface ISymbolRoundedSqureDonut : ISymbolEditedCorner
	{
		double OuterDiameter { get; }

		double InnerDiameter { get; }

		double Radius { get; }

		bool[] IsRounded { get; }
	}
}
