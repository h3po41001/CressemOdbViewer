using System.IO;
using System.IO.Compression;

namespace CressemExtractLibrary.Data
{
	internal class ExtractData
	{
		protected ExtractData() { }

		public ExtractData(string loadPath, string savePath)
		{
			SavePath = savePath;
			LoadPath = loadPath;
		}

		public string SavePath { get; private set; }

		public string LoadPath { get; private set; }
	}
}
