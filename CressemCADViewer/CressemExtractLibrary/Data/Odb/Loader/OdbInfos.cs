using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CressemExtractLibrary.Data.Odb.Loader
{
	internal class OdbInfos : OdbLoader
	{
		private static OdbInfos _instance;

		private OdbInfos() { }

		public static OdbInfos Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new OdbInfos();
				}

				return _instance;
			}
		}

		public string JobName { get; private set; }

		public string OdbVersionMajor { get; private set; }

		public string OdbVersionMinor { get; private set; }

		public string CreationData { get; private set; }

		public string SaveDate { get; private set; }

		public string SaveApp { get; private set; }

		public string SaveUser { get; private set; }

		public bool Load(string dirPath)
		{
			var miscFolder = Path.Combine(dirPath, MiscFolderName);
			if (Directory.Exists(miscFolder) is false)
			{
				return false;
			}

			var infoFilePath = Path.Combine(miscFolder, InfoFileName);
			if (File.Exists(infoFilePath) is false)
			{
				return false;
			}

			using (StreamReader reader = new StreamReader(infoFilePath))
			{
				string line = string.Empty;
				while (reader.EndOfStream is false)
				{
					line = reader.ReadLine().Trim();
					if (line.Contains("JOB_NAME") is true)
					{
						JobName = line.Split('=')[1].Trim().ToUpper();
					}
					else if (line.Contains("ODB_VERSION_MAJOR") is true)
					{
						OdbVersionMajor = line.Split('=')[1].Trim();
					}
					else if (line.Contains("ODB_VERSION_MINOR") is true)
					{
						OdbVersionMinor = line.Split('=')[1].Trim();
					}
					else if (line.Contains("CREATION_DATE") is true)
					{
						CreationData = line.Split('=')[1].Trim();
					}
					else if (line.Contains("SAVE_DATE") is true)
					{
						SaveDate = line.Split('=')[1].Trim();
					}
					else if (line.Contains("SAVE_APP") is true)
					{
						SaveApp = line.Split('=')[1].Trim();
					}
					else if (line.Contains("SAVE_USER") is true)
					{
						SaveUser = line.Split('=')[1].Trim();
					}
				}
			}

			return true;
		}
	}
}
