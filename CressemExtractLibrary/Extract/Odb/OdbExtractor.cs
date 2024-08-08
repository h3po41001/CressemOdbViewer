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

		public override bool Extract()
		{
			if (ExtractData is OdbData)
			{
				return true;
			}

			return false;
		}
	}
}
