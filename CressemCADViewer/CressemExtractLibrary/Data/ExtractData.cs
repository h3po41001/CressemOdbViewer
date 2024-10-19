using System.Drawing;

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

		public abstract RectangleF GetStepRoi(string stepName);
	}
}
