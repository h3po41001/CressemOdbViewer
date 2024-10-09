using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CressemExtractLibrary.Data.Odb.Font;

namespace CressemExtractLibrary.Data.Odb.Loader
{
	internal class OdbFontLoader : OdbLoader
	{
		private static OdbFontLoader _instance;

		private OdbFontLoader() { }

		public static OdbFontLoader Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new OdbFontLoader();
				}
				return _instance;
			}
		}

		public bool Load(string dirPath, out List<OdbFont> odbFonts)
		{
			odbFonts = null;
			
			string fontFolder = Path.Combine(dirPath, FontFolderName);
			if (Directory.Exists(fontFolder) is false)
			{
				return false;
			}

			odbFonts = new List<OdbFont>();

			string[] fontsPath = Directory.GetFiles(fontFolder);			
			foreach (var fontPath in fontsPath)
			{
				string line = string.Empty;
				string[] splited = null;

				string fontName = Path.GetFileName(fontPath);
				OdbFont odbFont = new OdbFont(fontName, fontPath);

				using (StreamReader reader = new StreamReader(fontPath))
				{
					bool isMM = false;
					double xSize = 0.0;
					double ySize = 0.0;
					double offset = 0.0;

					while (reader.EndOfStream is false)
					{
						line = reader.ReadLine().Trim();
						if (line.Contains("U MM") is true)
						{
							isMM = true;
						}

						if (line.Equals("") is true)
						{
							continue;
						}

						splited = line.Split(' ').Select(
							data => data.ToUpper()).Where(data => data != "").ToArray();

						if (splited[0].Equals("XSIZE") is true)
						{
							if (double.TryParse(splited[1], out xSize) is false)
							{
								continue;
							}
						}
						else if (splited[0].Equals("YSIZE") is true)
						{
							if (double.TryParse(splited[1], out ySize) is false)
							{
								continue;
							}
						}
						else if (splited[0].Equals("OFFSET") is true)
						{
							if (double.TryParse(splited[1], out offset) is false)
							{
								continue;
							}
						}
						else if (splited[0].Equals("CHAR") is true)
						{
							OdbFontAttr fontAttr = new OdbFontAttr(splited[1], isMM);

							double sx = 0.0;
							double sy = 0.0;
							double ex = 0.0;
							double ey = 0.0;
							string polarity = string.Empty;
							string shape = string.Empty;
							double width = 0.0;

							while (reader.EndOfStream is false)
							{
								line = reader.ReadLine().Trim();
								splited = line.Split(' ').Select(
									data => data.ToUpper()).ToArray();
								
								if (splited[0].Equals("ECHAR") is true)
								{
									odbFont.AddFontAttr(fontAttr);
									break;
								}

								if (splited[0].Equals("LINE") is true)
								{
									if (double.TryParse(splited[1], out sx) is false)
									{
										continue;
									}
									
									if (double.TryParse(splited[2], out sy) is false)
									{
										continue;
									}

									if (double.TryParse(splited[3], out ex) is false)
									{
										continue;
									}

									if (double.TryParse(splited[4], out ey) is false)
									{
										continue;
									}

									polarity = splited[5];
									shape = splited[6];

									if (double.TryParse(splited[7], out width) is false)
									{
										continue;
									}

									fontAttr.AddLine(new OdbFontLine(
										sx, sy, ex, ey, polarity, shape, width));
								}
							}
						}
					}
				}

				odbFonts.Add(odbFont);
			}

			return true;
		}
	}
}
