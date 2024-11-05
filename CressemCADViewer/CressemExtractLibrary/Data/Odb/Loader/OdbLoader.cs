using System.IO;

namespace CressemExtractLibrary.Data.Odb.Loader
{
	internal abstract class OdbLoader
	{
		private readonly string MISC_FOLDER_NAME = "misc";
		private readonly string INFO_FILE_NAME = "info";
		private readonly string ODB_SUM_FILE_EXTENSION = ".sum";
		private readonly string SYMBOLS_FOLDER_NAME = "symbols";
		private readonly string LAYERS_FOLDER_NAME = "layers";
		private readonly string STEP_FOLDER_NAME = "steps";
		private readonly string FONT_FOLDER_NAME = "fonts";
		private readonly string STEP_HEADER_FILE_NAME = "stephdr";
		private readonly string PROFILE_FILE_NAME = "profile";
		private readonly string MATRIX_FILE_NAME = "matrix";
		private readonly string FEATURES_FILE_NAME = "features";
		private readonly string COMPONENTS_FILE_NAME = "components";

		protected OdbLoader()
		{
		}

		protected string MiscFolderName { get => MISC_FOLDER_NAME; }

		protected string InfoFileName { get => INFO_FILE_NAME; }

		protected string SumFileExt { get => ODB_SUM_FILE_EXTENSION; }

		protected string SymbolsFolderName { get => SYMBOLS_FOLDER_NAME; }

		protected string LayersFolderName { get => LAYERS_FOLDER_NAME; }

		protected string StepFolderName { get => STEP_FOLDER_NAME; }

		protected string FontFolderName { get => FONT_FOLDER_NAME; }

		protected string StepHeaderFileName { get => STEP_HEADER_FILE_NAME; }

		protected string ProfileFileName { get => PROFILE_FILE_NAME; }

		protected string MatrixFileName { get => MATRIX_FILE_NAME; }

		protected string FeaturesFileName { get => FEATURES_FILE_NAME; }

		protected string ComponentsFileName { get => COMPONENTS_FILE_NAME; }

		protected bool LoadSummary(string path, out OdbSummary summary)
		{
			summary = null;

			if (File.Exists(path) is false)
			{
				return false;
			}

			using (StreamReader reader = new StreamReader(path))
			{
				int size = 0;
				int sum = 0;
				string date = string.Empty;
				string time = string.Empty;
				string version = string.Empty;
				string user = string.Empty;

				string line;
				string[] splited;

				while (!reader.EndOfStream)
				{
					line = reader.ReadLine().Trim();
					splited = line.Split('=');

					if (splited[0] == "SIZE")
					{
						if (int.TryParse(splited[1], out size) is false)
						{
							continue;
						}
					}
					else if (splited[0] == "SUM")
					{
						if (int.TryParse(splited[1], out sum) is false)
						{
							continue;
						}
					}
					else if (splited[0] == "DATE")
					{
						date = splited[1];
					}
					else if (splited[0] == "TIME")
					{
						time = splited[1];
					}
					else if (splited[0] == "VERSION")
					{
						version = splited[1];
					}
					else if (splited[0] == "USER")
					{
						user = splited[1];
					}
				}

				summary = new OdbSummary(size, sum,
					date, time, version, user);
			}

			return true;
		}
	}
}
