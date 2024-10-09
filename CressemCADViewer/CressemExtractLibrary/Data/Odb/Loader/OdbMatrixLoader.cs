using System.Collections.Generic;
using System.IO;
using System.Linq;
using CressemExtractLibrary.Data.Odb.Matrix;

namespace CressemExtractLibrary.Data.Odb.Loader
{
	internal class OdbMatrixLoader : OdbLoader
	{
		private static OdbMatrixLoader _instance;

		private OdbMatrixLoader() { }

		public static OdbMatrixLoader Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new OdbMatrixLoader();
				}

				return _instance;
			}
		}

		public bool Load(string dirPath, out OdbMatrixInfo matrixInfo)
		{
			matrixInfo = null;

			string matrixsumFilePath = Path.Combine(dirPath, MatrixFileName, 
				"." + MatrixFileName + SumFileExt);

			if (LoadSummary(matrixsumFilePath,
				out OdbSummary summary) is false)
			{
				return false;
			}

			string matrixFilePath = Path.Combine(dirPath, MatrixFileName, MatrixFileName);

			if (LoadMatrixStepLayer(matrixFilePath,
				out List<OdbMatrixStep> steps,
				out List<OdbMatrixLayer> layers) is false)
			{
				return false;
			}

			matrixInfo = new OdbMatrixInfo(summary, steps, layers);
			return true;
		}

		private bool LoadMatrixStepLayer(string path,
			out List<OdbMatrixStep> steps,
			out List<OdbMatrixLayer> layers)
		{
			steps = null;
			layers = null;

			if (File.Exists(path) is false)
			{
				return false;
			}

			using (StreamReader reader = new StreamReader(path))
			{
				string line;
				string[] splited;

				steps = new List<OdbMatrixStep>();
				layers = new List<OdbMatrixLayer>();

				while (reader.EndOfStream is false)
				{
					line = reader.ReadLine().Trim();

					if (line.ToUpper().Contains("STEP") is true)
					{
						int col = -1;
						string name = string.Empty;

						while (!reader.EndOfStream)
						{
							line = reader.ReadLine().Trim();
							if (line.Contains("}"))
							{
								break;
							}

							splited = line.Split('=');

							if (splited[0].Equals("COL") is true)
							{
								if (int.TryParse(splited[1], out col) is false)
								{
									continue;
								}
							}
							else if (splited[0].Equals("NAME") is true)
							{
								name = splited[1];
							}
						}

						steps.Add(new OdbMatrixStep(col, name));
					}
					else if (line.ToUpper().Contains("LAYER") is true)
					{
						int row = -1;
						string context = string.Empty;
						string type = string.Empty;
						string name = string.Empty;
						string oldName = string.Empty;
						bool polarity = false;
						string startName = string.Empty;
						string endName = string.Empty;
						string addName = string.Empty;

						while (!reader.EndOfStream)
						{
							line = reader.ReadLine().Trim();
							if (line.Contains("}"))
							{
								break;
							}

							splited = line.Split('=').Select(
								data => data.ToUpper()).ToArray();

							if (splited[0].Equals("ROW"))
							{
								if (int.TryParse(splited[1], out row) is false)
								{
									continue;
								}
							}
							else if (splited[0].Equals("CONTEXT"))
							{
								context = splited[1];
							}
							else if (splited[0].Equals("TYPE"))
							{
								type = splited[1];
							}
							else if (splited[0].Equals("NAME"))
							{
								name = splited[1];
							}
							else if (splited[0].Equals("OLD_NAME"))
							{
								oldName = splited[1];
							}
							else if (splited[0].Equals("POLARITY"))
							{
								polarity = splited[1].ToUpper().Equals("POSITIVE");
							}
							else if (splited[0].Equals("START_NAME"))
							{
								startName = splited[1];
							}
							else if (splited[0].Equals("END_NAME"))
							{
								endName = splited[1];
							}
							else if (splited[0].Equals("ADD_NAME"))
							{
								addName = splited[1];
							}
						}

						layers.Add(new OdbMatrixLayer(row, context, type,
							name, oldName, polarity, startName, endName, addName));
					}
				}
			}

			return true;
		}
	}
}
