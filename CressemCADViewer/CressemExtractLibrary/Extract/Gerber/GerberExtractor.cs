using CressemExtractLibrary.Data.Gerber;

namespace CressemExtractLibrary.Extract.Gerber
{
	internal class GerberExtractor : Extractor
	{
		public GerberExtractor(string loadPath, string savePath) : base()
		{ 
			ExtractData = new GerberData(loadPath, savePath);
		}

		public override bool OpenAndSave()
		{
			return false;
		}

		public override bool Load()
		{
			throw new System.NotImplementedException();
		}

		public override bool Extract()
		{
			if (ExtractData is GerberData)
			{
				return true;
			}

			return false;
		}

		public override bool DoWork()
		{
			return false;
		}
	}
}
