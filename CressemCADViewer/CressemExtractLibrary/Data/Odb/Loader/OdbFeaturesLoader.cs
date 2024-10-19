using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using CressemExtractLibrary.Data.Odb.Attribute;
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
			out OdbFeatures features)
		{
			features = new OdbFeatures();
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
						continue;
					}

					char firstChar = line.FirstOrDefault();
					if (firstChar.Equals('@') is true)  // attribute인 경우
					{
						continue;
					}
					else if (firstChar.Equals('&') is true)  // attribute text인 경우
					{
						continue;
					}
					else if (firstChar.Equals('#') is true)  // 주석인 경우
					{
						continue;
					}

					index++;

					if (firstChar.Equals('$') is true)  // Symbol인 경우
					{
						splited = line.Split(' ').Select(
							data => data.ToUpper()).ToArray();

						if (OdbSymbolLoader.Instance.LoadStandardSymbols(index, splited[1],
							out OdbSymbolBase odbFeatureSymbol) is false)
						{
							continue;
						}

						if (odbFeatureSymbol is null)
						{
							continue;
						}

						features.AddSymbol(odbFeatureSymbol);
					}
					else
					{
						// Feature인 경우
						splited = line.Split(' ').Select(
							data => data.ToUpper()).ToArray();

						// Line
						if (splited[0].Equals("L") is true)
						{
							var odbFeatureLine = OdbFeatureLine.Create(index, isMM, splited);
							if (odbFeatureLine is null)
							{
								continue;
							}

							features.AddFeature(odbFeatureLine);
						}
						else if (splited[0].Equals("P") is true)
						{
							var odbFeaturePad = OdbFeaturePad.Create(index, isMM, splited);
							if (odbFeaturePad is null)
							{
								continue;
							}

							features.AddFeature(odbFeaturePad);
						}
						else if (splited[0].Equals("A") is true)
						{
							var odbFeatureArc = OdbFeatureArc.Create(index, isMM, splited);
							if (odbFeatureArc is null)
							{
								continue;
							}

							features.AddFeature(odbFeatureArc);
						}
						// Text
						else if (splited[0].Equals("T") is true)
						{
							var odbFeatureText = OdbFeatureText.Create(index, isMM, splited);
							if (odbFeatureText is null)
							{
								continue;
							}

							features.AddFeature(odbFeatureText);
						}
						else if (splited[0].Equals("B") is true)
						{
							var odbFeatureBarcode = OdbFeatureBarcode.Create(index, isMM, splited);
							if (odbFeatureBarcode is null)
							{
								continue;
							}

							features.AddFeature(odbFeatureBarcode);
						}
						// Surface
						else if (splited[0].Equals("S") is true)
						{
							if (LoadSurface(index, splited, reader, isMM,
								out OdbFeatureSurface surface) is false)
							{
								continue;
							}

							features.AddFeature(surface);
						}
					}
				}
			}

			return features.FeatureList.Any();
		}

		public bool LoadSurface(int index, string[] firstLine, StreamReader reader, bool isMM,
			out OdbFeatureSurface surface)
		{
			surface = null;

			string polarity = firstLine[1];
			string decode = firstLine[2];

			surface = new OdbFeatureSurface(index, isMM, polarity, decode);

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
							surface.AddPolygon(new OdbSymbolPolygon(isMM, polygonAttrList));
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
