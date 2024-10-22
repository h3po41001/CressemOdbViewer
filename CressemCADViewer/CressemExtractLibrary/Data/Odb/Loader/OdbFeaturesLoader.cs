using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml.Schema;
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

			// Symbol
			using (StreamReader reader = new StreamReader(path))
			{
				string[] splited;
				int index = -1;
				bool isMM = false;

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
						var attrName = OdbAttributeName.Create(line);
						if (attrName is null)
						{
							continue;
						}

						features.AddAttributeName(attrName);
					}
					else if (firstChar.Equals('&') is true)  // attribute text인 경우
					{
						var attrText = OdbAttributeTextString.Create(line);
						if (attrText is null)
						{
							continue;
						}

						features.AddAttributeText(attrText);
					}
					else if (firstChar.Equals('#') is true)  // 주석인 경우
					{
						continue;
					}
					else if (firstChar.Equals('$') is true)  // Symbol인 경우
					{
						splited = line.Split(' ').Select(
							data => data.ToUpper()).ToArray();

						if (OdbSymbolLoader.Instance.LoadStandardSymbols(splited[0], splited[1], userSymbols,
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

						index++;

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
			string[] splited = firstLine[2].Split(';');

			string polarity = firstLine[1];
			string decode = splited[0];
			string attrString = splited.Length > 1 ? splited[1] : string.Empty;

			surface = new OdbFeatureSurface(index, isMM, polarity, decode, attrString);

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
					if (double.TryParse(splited[1], out double sx) is false)
					{
						continue;
					}

					if (double.TryParse(splited[2], out double sy) is false)
					{
						continue;
					}

					// I : Island, H : Hole
					string polyType = splited[3];

					//List<OdbPolygonAttr> polygonAttrList = new List<OdbPolygonAttr>
					//{
					//	new OdbPolygonOB(sbx, sby, polyType)
					//};

					OdbFeaturePolygon polygon = new OdbFeaturePolygon(polyType);

					// Define of one polygon
					while (reader.EndOfStream is false)
					{
						line = reader.ReadLine().Trim();
						splited = line.Split(' ').Select(
							data => data.ToUpper()).ToArray();

						if (splited[0].Equals("OE") is true)
						{
							surface.AddPolygon(polygon);
							break;
						}

						if (double.TryParse(splited[1], out double x) is false)
						{
							continue;
						}

						if (double.TryParse(splited[2], out double y) is false)
						{
							continue;
						}

						if (splited[0].Equals("OS") is true)
						{
							polygon.AddFeature(new OdbFeatureLine(index, isMM, 
								sx, sy, x, y, -1, "", "", ""));

							sx = x;
							sy = y;
						}
						else if (splited[0].Equals("OC") is true)
						{
							if (double.TryParse(splited[3], out double cx) is false)
							{
								continue;
							}

							if (double.TryParse(splited[4], out double cy) is false)
							{
								continue;
							}

							polygon.AddFeature(new OdbFeatureArc(index, isMM,
									sx, sy, x, y, cx, cy, -1, "", "", splited[5], ""));

							sx = x;
							sy = y;
						}
					}
				}
			}

			return true;
		}
	}
}
