using System.IO;
using System.IO.Compression;

namespace CressemExtractLibrary.Data
{
	internal class ExtractData
	{
		private string _loadPath = string.Empty;
		private string _savePath = string.Empty;

		protected ExtractData() { }

		public ExtractData(string loadPath, string savePath)
		{
			_loadPath = loadPath;
			_savePath = savePath;
		}

		public bool LoadData()
		{
			if (Directory.Exists(_savePath) is true)
			{
				Directory.Delete(_savePath, true);
			}

			var dir = Directory.CreateDirectory(_savePath);
			if (dir.Exists is false)
			{
				return false;
			}

			try
			{
				if (Path.GetExtension(_loadPath).ToUpper() == ".ZIP")
				{
					using (ZipArchive archive = ZipFile.Open(_loadPath, ZipArchiveMode.Update))
					{
						var names = _savePath.Split(Path.DirectorySeparatorChar);

						archive.ExtractToDirectory(_savePath);
					}

					return true;
				}
				//else if (Path.GetExtension(_savePath).ToUpper() == ".TGZ")
				//{
				//	using (Stream inStream = File.OpenRead(_savePath))
				//	{
				//		using (Stream gzipStream = new GZipInputStream(inStream))
				//		{
				//			TarArchive tarArchive = TarArchive.CreateInputTarArchive(gzipStream);
				//			tarArchive.ExtractContents(outPath);
				//		}
				//	}
				//}
				//else if (Path.GetExtension(path).ToUpper() == ".TAR")
				//{
				//	//using (Stream stream = File.OpenRead(path))
				//	//using (var reader = SharpCompress.Readers.ReaderFactory.Open(stream))
				//	//{
				//	//	while (reader.MoveToNextEntry())
				//	//	{
				//	//		if (!reader.Entry.IsDirectory)
				//	//		{
				//	//			Console.WriteLine(reader.Entry.Key);
				//	//			reader.WriteEntryToDirectory(outPath, new SharpCompress.Common.ExtractionOptions()
				//	//			{
				//	//				ExtractFullPath = true,
				//	//				Overwrite = true
				//	//			});
				//	//		}
				//	//	}
				//	//}

				//}
				else
				{
					return false;
				}
			}
			catch
			{
				return false;
			}
		}

		public void SaveData()
		{
			// TODO: Implement the logic to save data to the specified save path
		}
	}
}
