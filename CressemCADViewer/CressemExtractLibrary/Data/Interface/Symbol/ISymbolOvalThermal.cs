using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CressemExtractLibrary.Data.Interface.Symbol
{
	public interface ISymbolOvalThermal : ISymbolBase
	{
		double OuterWidth { get; }

		double OuterHeight { get; }

		double Angle { get; }

		int NumberOfSpoke { get; }

		double Gap { get; }

		double LineWidth { get; }
	}
}
