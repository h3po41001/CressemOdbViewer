using System.Collections.Generic;
using CressemExtractLibrary.Data.Interface.Features;

namespace CressemExtractLibrary.Data.Interface.Font
{
	public interface IFontAttribute
	{
		string Character { get; }

		bool IsMM { get; }

		IEnumerable<IFontLine> FontLines { get; }
	}
}
