using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CressemExtractLibrary.Data.Odb.Feature;
using CressemExtractLibrary.Data.Odb.Layer;
using CressemExtractLibrary.Data.Odb.Step;

namespace CressemExtractLibrary.Data.Odb.Loader
{
	internal class OdbStepLoader : OdbLoader
	{
		private static OdbStepLoader _instance;

		private OdbStepLoader() { }

		public static OdbStepLoader Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new OdbStepLoader();
				}
				return _instance;
			}
		}

		public bool Load(string dirPath, OdbData odbData,
			out List<OdbStep> odbSteps)
		{
			odbSteps = null;
			ConcurrentQueue<OdbStep> stepQueue = new ConcurrentQueue<OdbStep>();

			Parallel.ForEach(odbData.OdbMatrixInfo.Steps, step =>
			{
				string stepFolder = Path.Combine(dirPath, StepFolderName, step.Name);
				string stepHeaderPath = Path.Combine(stepFolder, StepHeaderFileName);
				string stepProfilePath = Path.Combine(stepFolder, ProfileFileName);
				string stepLayerPath = Path.Combine(stepFolder, LayersFolderName);

				if (LoadOdbStepHeader(stepHeaderPath,
					out OdbStepHeader stepHeader) is false)
				{
					return;
				}

				if (LoadOdbStepProfile(stepProfilePath, odbData,
					out OdbStepProfile stepProfile) is false)
				{
					return;
				}

				if (LoadOdbStepLayer(stepLayerPath, odbData,
					out List<OdbLayer> stepLayer) is false)
				{
					return;
				}

				stepQueue.Enqueue(new OdbStep(step, stepHeader, stepProfile, stepLayer));
			});

			odbSteps = new List<OdbStep>(stepQueue);

			return odbSteps.Any();
		}

		private bool LoadOdbStepHeader(string path, out OdbStepHeader stepHeader)
		{
			stepHeader = null;

			if (File.Exists(path) is false)
			{
				return false;
			}

			using (StreamReader reader = new StreamReader(path))
			{
				double xDatum = 0.0;
				double yDatum = 0.0;
				double xOrigin = 0.0;
				double yOrigin = 0.0;
				List<OdbStepRepeat> repeats = new List<OdbStepRepeat>();
				double leftActive = 0;
				double rightActive = 0;
				double topActive = 0;
				double bottomActive = 0;
				int affectingBom = 0;
				int affectingBomChanged = 0;

				string line;
				while (!reader.EndOfStream)
				{
					line = reader.ReadLine().Trim();
					if (line.Contains("STEP-REPEAT"))
					{
						string name = string.Empty;
						double x = 0.0;
						double y = 0.0;
						double dx = 0.0;
						double dy = 0.0;
						int nx = 0;
						int ny = 0;
						double angle = 0.0;
						bool isFlipHorizontal = false;

						string[] splited;

						while (!reader.EndOfStream)
						{
							line = reader.ReadLine().Trim();
							if (line.Contains("}"))
								break;

							splited = line.Split('=').Select(
								data => data.ToUpper()).ToArray();

							if (splited[0].Equals("NAME"))
							{
								name = splited[1];
							}
							else if (splited[0].Equals("X"))
							{
								if (double.TryParse(splited[1], out x) is false)
								{
									continue;
								}
							}
							else if (splited[0].Equals("Y"))
							{
								if (double.TryParse(splited[1], out y) is false)
								{
									continue;
								}
							}
							else if (splited[0].Equals("DX"))
							{
								if (double.TryParse(splited[1], out dx) is false)
								{
									continue;
								}
							}
							else if (splited[0].Equals("DY"))
							{
								if (double.TryParse(splited[1], out dy) is false)
								{
									continue;
								}
							}
							else if (splited[0].Equals("NX"))
							{
								if (int.TryParse(splited[1], out nx) is false)
								{
									continue;
								}
							}
							else if (splited[0].Equals("NY"))
							{
								if (int.TryParse(splited[1], out ny) is false)
								{
									continue;
								}
							}
							else if (splited[0].Equals("ANGLE"))
							{
								if (double.TryParse(splited[1], out angle) is false)
								{
									continue;
								}
							}
							else if (splited[0].Equals("MIRROR"))
							{
								isFlipHorizontal = splited[1].Equals("YES");
							}
						}

						repeats.Add(new OdbStepRepeat(name, x, y,
							dx, dy, nx, ny, angle, isFlipHorizontal));
					}
					else
					{
						string[] splited;

						splited = line.Split('=').Select(
							data => data.ToUpper()).ToArray();

						if (splited[0].Trim().Equals("X_DATUM"))
						{
							if (double.TryParse(splited[1], out xDatum) is false)
							{
								continue;
							}
						}
						else if (splited[0].Trim().Equals("Y_DATUM"))
						{
							if (double.TryParse(splited[1], out yDatum) is false)
							{
								continue;
							}
						}
						else if (splited[0].Trim().Equals("X_ORIGIN"))
						{
							if (double.TryParse(splited[1], out xOrigin) is false)
							{
								continue;
							}
						}
						else if (splited[0].Trim().Equals("Y_ORIGIN"))
						{
							if (double.TryParse(splited[1], out yOrigin) is false)
							{
								continue;
							}
						}
						else if (splited[0].Trim().Equals("TOP_ACTIVE"))
						{
							if (double.TryParse(splited[1], out topActive) is false)
							{
								continue;
							}
						}
						else if (splited[0].Trim().Equals("BOTTOM_ACTIVE"))
						{
							if (double.TryParse(splited[1], out bottomActive) is false)
							{
								continue;
							}
						}
						else if (splited[0].Trim().Equals("RIGHT_ACTIVE"))
						{
							if (double.TryParse(splited[1], out rightActive) is false)
							{
								continue;
							}
						}
						else if (splited[0].Trim().Equals("LEFT_ACTIVE"))
						{
							if (double.TryParse(splited[1], out leftActive) is false)
							{
								continue;
							}
						}
						else if (splited[0].Trim().Equals("AFFECTING_BOM"))
						{
							if (int.TryParse(splited[1], out affectingBom) is false)
							{
								continue;
							}
						}
						else if (splited[0].Trim().Equals(" AFFECTING_BOM_CHANGED"))
						{
							if (int.TryParse(splited[1], out affectingBomChanged) is false)
							{
								continue;
							}
						}
					}
				}

				stepHeader = new OdbStepHeader(xDatum, yDatum,
					xOrigin, yOrigin, repeats,
					topActive, bottomActive,
					rightActive, leftActive,
					affectingBom, affectingBomChanged);
			}

			return true;
		}

		private bool LoadOdbStepProfile(string path, OdbData odbData,
			out OdbStepProfile stepProfile)
		{
			stepProfile = null;

			if (File.Exists(path) is false)
			{
				return false;
			}

			if (OdbFeaturesLoader.Instance.Load(path, "",
				odbData.OdbUserSymbols, odbData.OdbFonts,
				out OdbFeatures features) is false)
			{
				return false;
			}

			stepProfile = new OdbStepProfile(features);

			return true;
		}

		private bool LoadOdbStepLayer(string path, OdbData odbData,
			out List<OdbLayer> stepLayer)
		{
			stepLayer = null;

			if (Directory.Exists(path) is false)
			{
				return false;
			}

			bool isL01 = odbData.OdbMatrixInfo.Layers.Any(x => x.Name.ToUpper().Equals("L01"));
			ConcurrentQueue<OdbLayer> odbLayersQueue = new ConcurrentQueue<OdbLayer>();

			Parallel.ForEach(odbData.OdbMatrixInfo.Layers, refLayer =>
			{
				string layerFilePath = Path.Combine(path, refLayer.Name, FeaturesFileName);
				if (File.Exists(layerFilePath) is false)
				{
					return;
				}

				if (isL01 is true && refLayer.Name.ToUpper().Equals("L01") is false)
				{
					return;
				}

				if (OdbFeaturesLoader.Instance.Load(layerFilePath, refLayer.Name,
					odbData.OdbUserSymbols, odbData.OdbFonts,
					out OdbFeatures features) is false)
				{
					return;
				}

				odbLayersQueue.Enqueue(new OdbLayer(refLayer, features));
			});

			stepLayer = new List<OdbLayer>(odbLayersQueue);
			return stepLayer.Any();
		}
	}
}
