using System;

namespace CressemUtil.Zip
{
	internal interface IZipArchive
	{
		ErrorType OpenAndSave(string filePath, string savePath);
	}
}
