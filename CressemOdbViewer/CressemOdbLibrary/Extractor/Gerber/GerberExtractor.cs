using CressemExtractLibrary.Data;
using CressemExtractLibrary.Data.Gerber;

namespace CressemExtractLibrary.Extractor.Gerber
{
	internal class GerberExtractor : IDataExtractor
	{
		private GerberData _data;

		public GerberExtractor()
		{ 
		}

		public bool ExtractData(ExtractData data)
		{
			_data = data as GerberData;
			if (_data == null)
			{
				return false;
			}

			return true;
		}
	}
}
