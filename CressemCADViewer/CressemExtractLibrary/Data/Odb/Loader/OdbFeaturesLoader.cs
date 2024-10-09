using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using CressemExtractLibrary.Data.Odb.Feature;
using CressemExtractLibrary.Data.Odb.Symbol;

namespace CressemExtractLibrary.Data.Odb.Loader
{
	internal class OdbFeaturesLoader : OdbLoader
	{
		private static OdbFeaturesLoader _instance;

		private OdbFeaturesLoader() { }

		public static OdbFeaturesLoader Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new OdbFeaturesLoader();
				}

				return _instance;
			}
		}

		public bool Load(string path, List<OdbSymbolUser> userSymbols,
			out List<OdbFeatures> features)
		{
			features = new List<OdbFeatures>();
			string line = string.Empty;

			using (StreamReader reader = new StreamReader(path))
			{
				string[] splited;
				bool isMM = false;
				int index = -1;

				while (reader.EndOfStream is false)
				{
					line = reader.ReadLine().Trim();
					if (line.Contains("U MM") is true)
					{
						isMM = true;
					}
					else if (line.Contains("@") is true)  // attribute인 경우
					{
						continue;
					}
					else if (line.Contains("&") is true)  // attribute text인 경우
					{
						continue;
					}
					else if (line.Contains("#") is true)  // 주석인 경우
					{
						continue;
					}
					else if (line.Contains("$") is true)  // Symbol인 경우
					{
						splited = line.Split(' ').Select(
							data => data.ToUpper()).ToArray();

						if (OdbSymbolLoader.Instance.LoadStandardSymbols(splited[1], userSymbols, isMM,
							out OdbFeatures odbFeatureSymbol) is false)
						{
							continue;
						}

						features.Add(odbFeatureSymbol);
					}
					else
					{
						// Feature인 경우
						splited = line.Split(' ').Select(
							data => data.ToUpper()).ToArray();

						// Surface
						if (splited[0].Equals("S"))
						{
							if (LoadSurface(splited, reader, isMM,
								out OdbFeatureSurface surface) is false)
							{
								continue;
							}

							features.Add(surface);
						}
					}
				}
			}

			return features.Any();
		}

		public bool LoadSurface(string[] firstLine, StreamReader reader, bool isMM,
			out OdbFeatureSurface surface)
		{
			surface = null;
			if (firstLine[0].Equals("S") is false)
			{
				return false;
			}

			string polarity = firstLine[1];
			string decode = firstLine[2];

			surface = new OdbFeatureSurface(polarity, decode);

			string[] splited = null;
			string line = string.Empty;

			while (reader.EndOfStream is false)
			{
				line = reader.ReadLine().Trim();
				splited = line.Split(' ').Select(
					data => data.ToUpper()).ToArray();

				if (splited[0].Equals("SE") is true)
				{
					break;
				}

				if (splited[0].Equals("OB") is true)
				{
					if (double.TryParse(splited[1], out double xbs) is false)
					{
						continue;
					}

					if (double.TryParse(splited[2], out double ybs) is false)
					{
						continue;
					}

					string polyType = splited[3];                        // I : Island, H : Hole

					List<OdbPolygonAttr> polygonAttrList = new List<OdbPolygonAttr>
					{
						new OdbPolygonOB(xbs, ybs, polyType, isMM)
					};

					// Define of one polygon
					while (reader.EndOfStream is false)
					{
						line = reader.ReadLine().Trim();
						splited = line.Split(' ').Select(
							data => data.ToUpper()).ToArray();

						if (splited[0].Equals("OE") is true)
						{
							surface.Polygons.Add(new OdbFeaturePolygon(isMM, polygonAttrList));
							break;
						}

						if (splited[0].Equals("OS") is true)
						{
							if (double.TryParse(splited[1], out double x) is false)
							{
								continue;
							}

							if (double.TryParse(splited[2], out double y) is false)
							{
								continue;
							}

							polygonAttrList.Add(new OdbPolygonOS(x, y, isMM));
						}
						else if (splited[0].Equals("OC") is true)
						{
							if (double.TryParse(splited[1], out double ex) is false)
							{
								continue;
							}

							if (double.TryParse(splited[2], out double ey) is false)
							{
								continue;
							}

							if (double.TryParse(splited[3], out double cx) is false)
							{
								continue;
							}

							if (double.TryParse(splited[4], out double cy) is false)
							{
								continue;
							}

							bool isClockWise = splited[5].Equals("Y");
							polygonAttrList.Add(new OdbPolygonOC(ex, ey, cx, cy, isClockWise, isMM));
						}
					}
				}
			}

			return true;
		}
	}
}
