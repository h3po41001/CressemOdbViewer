using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using CressemExtractLibrary.Data.Odb;
using CressemExtractLibrary.Data.Odb.Matrix;
using CressemExtractLibrary.Data.Odb.Step;
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
					if (Load() is true)
					{
						if (DoWork() is true)
						{
							return true;
						}
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
					// features.Z 압축 해제
					var zPaths = Directory.GetFiles(ExtractData.SavePath,
						"*features.Z", SearchOption.AllDirectories);

					if (zPaths is null)
					{
						return true;
					}

					Parallel.ForEach(zPaths, zPath =>
					{
						var zPathLocal = Path.Combine(Path.GetDirectoryName(zPath),
							Path.GetFileNameWithoutExtension(zPath));

						if (Directory.Exists(zPathLocal))
						{
							Directory.Delete(zPathLocal, true);
						}

						error = ZipUtil.OpenAndSave(zPath, zPathLocal);
						if (error != ErrorType.None)
						{
							throw new System.Exception(error.ToString());
						}
					});

					return true;
				}
				else
				{
					MessageBox.Show($"[{error}] - {ExtractData.LoadPath}", 
						"압축해제 에러", MessageBoxButton.OK, MessageBoxImage.Error);
					return false;
				}
			}
			catch (System.Exception error)
			{
				MessageBox.Show($"[{error}] - {ExtractData.LoadPath}",
					"압축해제 에러", MessageBoxButton.OK, MessageBoxImage.Error);
				return false; 
			}
		}

		public override bool Load()
		{
			if (Directory.Exists(ExtractData.SavePath) is false)
			{
				return false;
			}

			if (ExtractData is OdbData odbData)
			{
				var name = (new DirectoryInfo(ExtractData.SavePath)).Name;
				var dirPath = Path.Combine(ExtractData.SavePath, name);

				odbData.OdbMatrixInfo = OdbMatrixLoader.Instance.Load(dirPath);
				if (odbData.OdbMatrixInfo is null)
				{
					return false;
				}

				foreach (var stepInfo in odbData.OdbMatrixInfo.Steps)
				{
					OdbStep step = OdbStepLoader.Instance.Load(dirPath, stepInfo);
					if (step is null)
					{
						return false;
					}

					odbData.OdbSteps.Add(step);
				}

				return true;
			}
			else
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
