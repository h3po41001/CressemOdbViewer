using System.IO;
using ICSharpCode.SharpZipLib.Lzw;

namespace CressemUtil.Zip.Extension
{
	internal class ZFileArchiveExtension : IZipArchive
	{
		public ZFileArchiveExtension() { }

		public ErrorType OpenAndSave(string filePath, string savePath)
		{
			if (File.Exists(filePath) is false)
			{
				return ErrorType.NotExistsFile;
			}

			using (Stream inStream = File.OpenRead(filePath))
			{
				if (inStream is null)
				{
					return ErrorType.NotSupportArchive;
				}

				using (Stream lzStream = new LzwInputStream(inStream))
				{
					if (lzStream is null)
					{
						return ErrorType.NotSupportArchive;
					}

					if (Directory.Exists(savePath))
					{
						Directory.Delete(savePath, true);
					}

					using (FileStream outStream = File.Create(savePath))
					{
						if (outStream is null)
						{
							return ErrorType.NotSupportArchive;
						}

						int sourceBytes;
						byte[] buffer = new byte[4096];

						do
						{
							sourceBytes = lzStream.Read(buffer, 0, buffer.Length);
							outStream.Write(buffer, 0, sourceBytes);
						} while (sourceBytes > 0);
					}
				}
			}

			return ErrorType.None;
		}
	}
}
