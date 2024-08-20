using System.IO;
using System.IO.Compression;

namespace CressemUtil.Zip.Extension
{
	internal class ZipArchiveExtension : IZipArchive
	{
		public ZipArchiveExtension() { }

		public ErrorType OpenAndSave(string filePath, string savePath)
		{
			using (var archive = ZipFile.Open(filePath, ZipArchiveMode.Update))
			{
				if (archive is null)
				{
					return ErrorType.NotSupportArchive;
				}
				
				archive.ExtractToDirectory(savePath);
			}

			if (Directory.Exists(savePath) is false)
			{
				return ErrorType.NotExistsDirectory;
			}

			return ErrorType.None;
		}
	}
}
