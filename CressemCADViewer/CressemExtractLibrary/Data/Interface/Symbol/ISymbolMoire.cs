using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CressemExtractLibrary.Data.Interface.Symbol
{
	public interface ISymbolMoire : ISymbolBase
	{
		double RingWidth { get; }

		double RingHeight { get; }

		int NumberOfRing { get; }

		double LineWidth { get; }

		double LineLength { get; }

		double LineAngle { get; }
	}
}
