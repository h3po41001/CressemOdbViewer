using CressemExtractLibrary.Data;
using CressemExtractLibrary.Data.Gerber;

namespace CressemExtractLibrary.Extract.Gerber
{
	internal class GerberExtractor : Extractor
	{
		public GerberExtractor(string loadPath, string savePath) : base()
		{ 
			ExtractData = new GerberData(loadPath, savePath);
		}

		public override bool Extract()
		{
			if (ExtractData is GerberData)
			{
				return false;
			}

			return true;
		}
	}
}
