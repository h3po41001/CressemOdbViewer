using CressemExtractLibrary.Data;
using CressemExtractLibrary.Extract;
using CressemExtractLibrary.Extract.Gerber;
using CressemExtractLibrary.Extract.Odb;

namespace CressemExtractLibrary
{
	public class ExtractLibrary
	{
		private static ExtractLibrary _instance;
		private Extractor _extractor;

		private ExtractLibrary() { }

		public static ExtractLibrary Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new ExtractLibrary();
				}

				return _instance;
			}
		}

		public bool SetData(DesignFormat dataFormat,
			string loadPath, string savePath)
		{
			switch (dataFormat)
			{
				case DesignFormat.Gerber:
					{
						_extractor = new GerberExtractor(loadPath, savePath);
						break;
					}
				case DesignFormat.Odb:
					{
						_extractor = new OdbExtractor(loadPath, savePath);
						break;
					}
				default:
					{
						_extractor = null;
						return false;
					}
			}

			return true;
		}

		public bool Extract()
		{
			if (_extractor is null)
				return false;

			return _extractor.Extract();
		}
	}
}
