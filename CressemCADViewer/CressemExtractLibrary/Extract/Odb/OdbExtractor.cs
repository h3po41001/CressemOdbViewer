using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using CressemExtractLibrary.Data.Odb;
using CressemExtractLibrary.Data.Odb.Font;
using CressemExtractLibrary.Data.Odb.Loader;
using CressemExtractLibrary.Data.Odb.Matrix;
using CressemExtractLibrary.Data.Odb.Step;
using CressemExtractLibrary.Data.Odb.Symbol;
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
				var name = (new DirectoryInfo(odbData.SavePath)).Name;
				var dirPath = Path.Combine(odbData.SavePath, name);

				//1. Matrix
				if (OdbMatrixLoader.Instance.Load(dirPath, 
					out OdbMatrixInfo matrixInfo) is false)
				{
					return false;
				}

				odbData.OdbMatrixInfo = matrixInfo;

				//2. Font
				if (OdbFontLoader.Instance.Load(dirPath,
					out List<OdbFont> fonts) is false)
				{
					return false;
				}

				odbData.OdbFonts = new List<OdbFont>(fonts);

				//3. User Symbols				
				if (OdbSymbolLoader.Instance.LoadUserSymbols(dirPath, 
					out List<OdbSymbolUser> userSymbols) is false)
				{
					return false;
				}

				odbData.OdbUserSymbols = new List<OdbSymbolUser>(userSymbols);

				//4. Steps
				if (OdbStepLoader.Instance.Load(dirPath, odbData,
					out List<OdbStep> odbSteps) is false)
				{
					return false;
				}

				odbData.OdbSteps = new List<OdbStep>(odbSteps);
				return true;
			}
			else
			{
				return false;
			}
		}

		public override bool DoWork()
		{
			return true;
		}
	}
}
