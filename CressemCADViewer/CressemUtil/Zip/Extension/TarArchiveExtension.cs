using System;
using System.IO;
using System.Text;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Tar;

namespace CressemUtil.Zip.Extension
{
	internal class TarArchiveExtension : IZipArchive
	{
		private TarArchive _archive;

		public TarArchiveExtension() { }

		public ErrorType OpenAndSave(string filePath, string savePath)
		{
			using (Stream inStream = File.OpenRead(filePath))
			{
				using (Stream gzipStream = new GZipInputStream(inStream))
				{
					_archive = TarArchive.CreateInputTarArchive(gzipStream, Encoding.ASCII);
					if (_archive is null)
					{
						return ErrorType.NotSupportArchive;
					}

					_archive.SetKeepOldFiles(false);
					_archive.ExtractContents(savePath, true);
				}
			}

			if (Directory.Exists(savePath) is false)
			{
				return ErrorType.NotExistsDirectory;
			}

			return ErrorType.None;
		}
	}
}