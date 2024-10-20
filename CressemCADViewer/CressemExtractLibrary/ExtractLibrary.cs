using System.Drawing;
using CressemExtractLibrary.Data;
using CressemExtractLibrary.Data.Odb.Feature;
using CressemExtractLibrary.Extract;
using CressemExtractLibrary.Extract.Gerber;
using CressemExtractLibrary.Extract.Odb;

namespace CressemExtractLibrary
{
	public class ExtractLibrary
	{
		private static ExtractLibrary _instance;
		private Extractor _extractor;

		private ExtractLibrary() { }

		public static ExtractLibrary Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new ExtractLibrary();
				}

				return _instance;
			}
		}

		public bool SetData(DesignFormat dataFormat,
			string loadPath, string savePath)
		{
			switch (dataFormat)
			{
				case DesignFormat.Gerber:
					{
						_extractor = new GerberExtractor(loadPath, savePath);
						break;
					}
				case DesignFormat.Odb:
					{
						_extractor = new OdbExtractor(loadPath, savePath);
						break;
					}
				default:
					{
						_extractor = null;
						return false;
					}
			}

			return true;
		}

		public bool Extract()
		{
			if (_extractor is null)
				return false;

			return _extractor.Extract();
		}

		public string[] GetStepNames()
		{
			if (_extractor is null)
				return null;

			return _extractor.ExtractData.GetStepNames();
		}

		public string[] GetLayerNames(string stepName)
		{
			if (_extractor is null)
				return null;

			return _extractor.ExtractData.GetLayerNames(stepName);
		}

		public RectangleF GetStepRoi(string stepName)
		{
			if (_extractor is null)
				return new RectangleF();

			return _extractor.ExtractData.GetStepRoi(stepName);
		}

		public OdbFeatureBase[] GetFeatures(string stepName, string layerName)
		{
			if (_extractor is null)
			{
				return null;
			}

			if (string.IsNullOrEmpty(stepName) || string.IsNullOrEmpty(layerName))
			{
				return null;
			}

			return _extractor.ExtractData.GetFeatures(stepName, layerName);
		}
	}
}
