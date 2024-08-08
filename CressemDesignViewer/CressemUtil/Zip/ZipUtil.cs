using System.IO;
using CressemUtil.Zip.Extension;

namespace CressemUtil.Zip
{
	public static class ZipUtil
	{
		private static IZipArchive _archive;

		public static ErrorType OpenAndSave(string filePath, string loadPath)
		{
			if (File.Exists(filePath) is false)
			{
				return ErrorType.NotExistsFile;
			}

			var dir = Directory.CreateDirectory(loadPath);
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
			else
			{
				return ErrorType.NotSupportExtension;
			}

			return _archive.OpenAndSave(filePath, loadPath);
		}
	}
}
