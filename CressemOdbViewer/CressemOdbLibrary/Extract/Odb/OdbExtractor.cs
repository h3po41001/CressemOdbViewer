using CressemExtractLibrary.Data;
using CressemExtractLibrary.Data.Odb;

namespace CressemExtractLibrary.Extract.Odb
{
	internal class OdbExtractor : Extractor
	{
		public OdbExtractor(string loadPath, string savePath) : base()
		{ 
			ExtractData = new OdbData(loadPath, savePath);
		}

		public override bool Extract(ExtractData data)
		{
			ExtractData = data as OdbData;
			if (ExtractData == null)
			{
				return false;
			}

			return true;
		}
	}
}
