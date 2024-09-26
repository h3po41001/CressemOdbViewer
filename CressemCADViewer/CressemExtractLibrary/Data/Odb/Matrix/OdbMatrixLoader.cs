using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace CressemExtractLibrary.Data.Odb.Matrix
{
	internal class OdbMatrixLoader : OdbLoader
	{
		private static OdbMatrixLoader _instance;

		private readonly string MATRIX_FILE_NAME = "matrix";
		private readonly string MATRIX_SUM_FILE_NAME = ".matrix.sum";

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

		public OdbMatrixInfo Load(string dirPath)
		{
			string matrixsumFilePath = Path.Combine(dirPath, MATRIX_FILE_NAME, MATRIX_SUM_FILE_NAME);
			if (LoadSummary(matrixsumFilePath,
				out OdbSummary summary) is false)
			{
				return null;
			}

			string matrixFilePath = Path.Combine(dirPath, MATRIX_FILE_NAME, MATRIX_FILE_NAME);
			if (LoadMatrixStepLayer(matrixFilePath,
				out List<OdbMatrixStep> steps,
				out List<OdbMatrixLayer> layers) is false)
			{
				return null;
			}

			return new OdbMatrixInfo(summary, steps, layers);
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
				string[] arrSplited;

				steps = new List<OdbMatrixStep>();
				layers = new List<OdbMatrixLayer>();

				while (!reader.EndOfStream)
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
								break;

							arrSplited = line.Split('=');

							if (arrSplited[0].Equals("COL") is true)
							{
								col = System.Convert.ToInt32(arrSplited[1]);
							}
							else if (arrSplited[0].Equals("NAME") is true)
							{
								name = arrSplited[1];
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
								break;

							arrSplited = line.Split('=').Select(
								data => data.ToUpper()).ToArray();

							if (arrSplited[0].Equals("ROW"))
							{
								row = System.Convert.ToInt32(arrSplited[1]);
							}
							else if (arrSplited[0].Equals("CONTEXT"))
							{
								context = arrSplited[1];
							}
							else if (arrSplited[0].Equals("TYPE"))
							{
								type = arrSplited[1];
							}
							else if (arrSplited[0].Equals("NAME"))
							{
								name = arrSplited[1];
							}
							else if (arrSplited[0].Equals("OLD_NAME"))
							{
								oldName = arrSplited[1];
							}
							else if (arrSplited[0].Equals("POLARITY"))
							{
								polarity = arrSplited[1].ToUpper().Equals("POSITIVE");
							}
							else if (arrSplited[0].Equals("START_NAME"))
							{
								startName = arrSplited[1];
							}
							else if (arrSplited[0].Equals("END_NAME"))
							{
								endName = arrSplited[1];
							}
							else if (arrSplited[0].Equals("ADD_NAME"))
							{
								addName = arrSplited[1];
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
