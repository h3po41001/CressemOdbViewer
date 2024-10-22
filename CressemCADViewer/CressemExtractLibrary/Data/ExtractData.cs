using CressemExtractLibrary.Data.Interface.Features;

namespace CressemExtractLibrary.Data
{
	internal abstract class ExtractData
	{
		protected ExtractData() { }

		public ExtractData(string loadPath, string savePath)
		{
			SavePath = savePath;
			LoadPath = loadPath;
		}

		public string SavePath { get; private set; }

		public string LoadPath { get; private set; }

		public abstract string[] GetStepNames();

		public abstract string[] GetLayerNames(string stepName);

		public abstract IFeatureBase GetStepRoi(string stepName);

		public abstract IFeatureBase[] GetFeatures(string stepName, string layerName,
			out double xDatum, out double yDatum);
	}
}
