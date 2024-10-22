using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CressemExtractLibrary.Data.Interface.Symbol
{
	public interface ISymbolHalfOval : ISymbolBase
	{
		double Width { get; }

		double Height { get; }
	}
}
