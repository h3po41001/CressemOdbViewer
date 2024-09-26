using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Documents;
using CressemExtractLibrary.Convert;
using CressemExtractLibrary.Data.Odb.Feature;
using CressemExtractLibrary.Data.Odb.Matrix;

namespace CressemExtractLibrary.Data.Odb.Step
{
	internal class OdbStepLoader : OdbLoader
	{
		private static OdbStepLoader _instance;

		private readonly string STEP_FILE_NAME = "steps";
		private readonly string STEP_HEADER_FILE_NAME = "stephdr";
		private readonly string STEP_PROFILE_FILE_NAME = "profile";

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

		public OdbStep Load(string dirPath, OdbMatrixStep step)
		{
			string stepFolder = Path.Combine(dirPath, STEP_FILE_NAME, step.Name);
			string stepHeaderPath = Path.Combine(stepFolder, STEP_HEADER_FILE_NAME);
			string stepProfilePath = Path.Combine(stepFolder, STEP_PROFILE_FILE_NAME);

			if (LoadOdbStepHeader(stepHeaderPath,
				out OdbStepHeader stepHeader) is false)
			{
				return null;
			}

			if (LoadOdbStepProfile(stepProfilePath, out OdbStepProfile stepProfile) is false)
			{
				return null;
			}

			return new OdbStep(step, stepHeader, stepProfile);
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
						bool isFliped = false;
						bool isMirrored = false;

						string[] arrSplited;

						while (!reader.EndOfStream)
						{
							line = reader.ReadLine().Trim();
							if (line.Contains("}"))
								break;

							arrSplited = line.Split('=').Select(
								data => data.ToUpper()).ToArray();

							if (arrSplited[0].Equals("NAME"))
							{
								name = arrSplited[1];
							}
							else if (arrSplited[0].Equals("X"))
							{
								x = System.Convert.ToDouble(arrSplited[1]);
								x = Converter.Instance.ConvertInchToMM(x);
							}
							else if (arrSplited[0].Equals("Y"))
							{
								y = System.Convert.ToDouble(arrSplited[1]);
								y = Converter.Instance.ConvertInchToMM(y);
							}
							else if (arrSplited[0].Equals("DX"))
							{
								dx = System.Convert.ToDouble(arrSplited[1]);
								dx = Converter.Instance.ConvertInchToMM(dx);
							}
							else if (arrSplited[0].Equals("DY"))
							{
								dy = System.Convert.ToDouble(arrSplited[1]);
								dy = Converter.Instance.ConvertInchToMM(dy);
							}
							else if (arrSplited[0].Equals("NX"))
							{
								nx = System.Convert.ToInt32(arrSplited[1]);
							}
							else if (arrSplited[0].Equals("NY"))
							{
								ny = System.Convert.ToInt32(arrSplited[1]);
							}
							else if (arrSplited[0].Equals("ANGLE"))
							{
								angle = System.Convert.ToDouble(arrSplited[1]);
							}
							else if (arrSplited[0].Equals("FLIP"))
							{
								isFliped = arrSplited[1].Equals("YES");
							}
							else if (arrSplited[0].Equals("MIRROR"))
							{
								isMirrored = arrSplited[1].Equals("YES");
							}
						}

						repeats.Add(new OdbStepRepeat(name, x, y,
							dx, dy, nx, ny, angle, isFliped, isMirrored));
					}
					else
					{
						string[] arrSplited;

						arrSplited = line.Split('=').Select(
							data => data.ToUpper()).ToArray();

						if (arrSplited[0].Trim().Equals("X_DATUM"))
						{
							xDatum = System.Convert.ToDouble(arrSplited[1]);
							xDatum = Converter.Instance.ConvertInchToMM(xDatum);
						}
						else if (arrSplited[0].Trim().Equals("Y_DATUM"))
						{
							yDatum = System.Convert.ToDouble(arrSplited[1]);
							yDatum = Converter.Instance.ConvertInchToMM(yDatum);
						}
						else if (arrSplited[0].Trim().Equals("X_ORIGIN"))
						{
							xOrigin = System.Convert.ToDouble(arrSplited[1]);
							xOrigin = Converter.Instance.ConvertInchToMM(xOrigin);
						}
						else if (arrSplited[0].Trim().Equals("Y_ORIGIN"))
						{
							yOrigin = System.Convert.ToDouble(arrSplited[1]);
							yOrigin = Converter.Instance.ConvertInchToMM(yOrigin);
						}
						else if (arrSplited[0].Trim().Equals("TOP_ACTIVE"))
						{
							topActive = System.Convert.ToDouble(arrSplited[1]);
							topActive = Converter.Instance.ConvertInchToMM(topActive);
						}
						else if (arrSplited[0].Trim().Equals("BOTTOM_ACTIVE"))
						{
							bottomActive = System.Convert.ToDouble(arrSplited[1]);
							bottomActive = Converter.Instance.ConvertInchToMM(bottomActive);
						}
						else if (arrSplited[0].Trim().Equals("RIGHT_ACTIVE"))
						{
							rightActive = System.Convert.ToDouble(arrSplited[1]);
							rightActive = Converter.Instance.ConvertInchToMM(rightActive);
						}
						else if (arrSplited[0].Trim().Equals("LEFT_ACTIVE"))
						{
							leftActive = System.Convert.ToDouble(arrSplited[1]);
							leftActive = Converter.Instance.ConvertInchToMM(leftActive);
						}
						else if (arrSplited[0].Trim().Equals("AFFECTING_BOM"))
						{
							affectingBom = System.Convert.ToInt32(arrSplited[1]);
						}
						else if (arrSplited[0].Trim().Equals(" AFFECTING_BOM_CHANGED"))
						{
							affectingBomChanged = System.Convert.ToInt32(arrSplited[1]);
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

		private bool LoadOdbStepProfile(string path, out OdbStepProfile stepProfile)
		{
			stepProfile = null;

			if (File.Exists(path) is false)
			{
				return false;
			}

			using (StreamReader reader = new StreamReader(path))
			{
				string line;
				string[] arrSplited;

				bool isMM = false;
				bool polarity = false;
				List<Tuple<double, double>> points = new List<Tuple<double, double>>();

				while (!reader.EndOfStream)
				{
					double x = 0.0;
					double y = 0.0;

					line = reader.ReadLine().Trim();
					if (line.Contains("SE") is true)
						break;

					if (line.Contains("U MM") is true)
					{
						isMM = true;
					}
					else if (line.Contains("S") is true)
					{
						arrSplited = line.Split(' ').Select(
							data => data.ToUpper()).ToArray();

						polarity = arrSplited[1].Equals("P");
					}
					else if (line.Contains("OB") is true)
					{
						arrSplited = line.Split(' ').Select(
							data => data.ToUpper()).ToArray();

						x = System.Convert.ToDouble(arrSplited[1]);
						if (isMM is false)
							x = Converter.Instance.ConvertInchToMM(x);

						y = System.Convert.ToDouble(arrSplited[2]);
						if (isMM is false)
							y = Converter.Instance.ConvertInchToMM(y);

						points.Add(new Tuple<double, double>(x, y));

						while (!reader.EndOfStream)
						{
							line = reader.ReadLine().Trim();
							if (line.Contains("OE") is true)
								break;

							if (line.Contains("OS") is true)
							{
								arrSplited = line.Split(' ').Select(
									data => data.ToUpper()).ToArray();

								x = System.Convert.ToDouble(arrSplited[1]);
								if (isMM is false)
									x = Converter.Instance.ConvertInchToMM(x);

								y = System.Convert.ToDouble(arrSplited[2]);
								if (isMM is false)
									y = Converter.Instance.ConvertInchToMM(y);

								points.Add(new Tuple<double, double>(x, y));
							}
						}
					}
				}

				stepProfile = new OdbStepProfile(new OdbFeaturePolygon(points));
			}

			return true;
		}
	}
}
