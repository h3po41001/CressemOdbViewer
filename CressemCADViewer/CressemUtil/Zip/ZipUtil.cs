using System.IO;
using CressemUtil.Zip.Extension;

namespace CressemUtil.Zip
{
	public static class ZipUtil
	{
		private static IZipArchive _archive;

		public static ErrorType OpenAndSave(string filePath, string savePath)
		{
			if (File.Exists(filePath) is false)
			{
				return ErrorType.NotExistsFile;
			}

			var dir = Directory.CreateDirectory(savePath);
			if (dir.Exists is false)
			{
				return ErrorType.NotExistsDirectory;
			}

			if (Path.GetExtension(filePath).ToUpper() == ".ZIP")
			{
				_archive = new ZipArchiveExtension();
			}
			else if (Path.GetExtension(filePath).ToUpper() == ".TGZ")
			{
				_archive = new TarArchiveExtension();
			}
			else if (Path.GetExtension(filePath).ToUpper() == ".Z")
			{
				_archive = new ZFileArchiveExtension();
			}
			else
			{
				return ErrorType.NotSupportExtension;
			}

			return _archive.OpenAndSave(filePath, savePath);
		}
	}
}
