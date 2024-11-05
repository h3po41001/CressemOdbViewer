using System.Collections.Generic;

namespace CressemExtractLibrary.Data.Interface.Font
{
	public interface IFont
	{
		string Name { get; }

		string Path { get; }

		double XSize { get; }

		double YSize { get; }

		double Offset { get; }

		IEnumerable<IFontAttribute> FontAttrs { get; }
	}
}
