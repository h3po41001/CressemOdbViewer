using System.IO;
using System.Windows;
using CressemExtractLibrary.Data.Odb;
using CressemUtil.Zip;

namespace CressemExtractLibrary.Extract.Odb
{
	internal class OdbExtractor : Extractor
	{
		public OdbExtractor(string loadPath, string savePath) : base()
		{
			ExtractData = new OdbData(loadPath, savePath);
		}

		public override bool Extract()
		{
			if (ExtractData is OdbData)
			{
				if (OpenAndSave() is true)
				{
					if (DoWork() is true)
					{
						return true;
					}
				}		
			}

			return false;
		}

		public override bool OpenAndSave()
		{
			try
			{
				if (Directory.Exists(ExtractData.SavePath) is true)
				{
					Directory.Delete(ExtractData.SavePath, true);
				}

				var error = ZipUtil.OpenAndSave(ExtractData.LoadPath, ExtractData.SavePath);

				if (error is ErrorType.None)
				{
					return true;
				}
				else
				{
					MessageBox.Show(error.ToString(), "압축해제 에러", MessageBoxButton.OK, MessageBoxImage.Error);
					return false;
				}
			}
			catch
			{
				return false;
			}
		}

		public override bool DoWork()
		{
			return false;
		}
	}
}
