using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CressemExtractLibrary.Data.Odb
{
	internal abstract class OdbLoader
	{
		private readonly string ODB_SUM_FILE_EXTENSION = ".sum";

		protected OdbLoader()
		{
		}

		protected string SumFileExt { get => ODB_SUM_FILE_EXTENSION; }

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
				string[] arrSplited;
				
				while (!reader.EndOfStream)
				{
					line = reader.ReadLine().Trim();
					arrSplited = line.Split('=');

					if (arrSplited[0] == "SIZE")
						size = System.Convert.ToInt32(arrSplited[1]);
					else if (arrSplited[0] == "SUM")
						sum = System.Convert.ToInt32(arrSplited[1]);
					else if (arrSplited[0] == "DATE")
						date = arrSplited[1];
					else if (arrSplited[0] == "TIME")
						time = arrSplited[1];
					else if (arrSplited[0] == "VERSION")
						version = arrSplited[1];
					else if (arrSplited[0] == "USER")
						user = arrSplited[1];
				}

				summary = new OdbSummary(size, sum, 
					date, time, version, user);
			}

			return true;
		}
	}
}
