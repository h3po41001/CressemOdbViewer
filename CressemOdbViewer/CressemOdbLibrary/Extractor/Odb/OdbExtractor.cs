using CressemExtractLibrary.Data;
using CressemExtractLibrary.Data.Odb;

namespace CressemExtractLibrary.Extractor.Odb
{
	internal class OdbExtractor : IDataExtractor
	{
		private OdbData _data;

		public OdbExtractor()
		{ 
		}

		public bool ExtractData(ExtractData data)
		{
			_data = data as OdbData;
			if (_data == null)
			{
				return false;
			}

			return true;
		}
	}
}
