using System.Drawing;
using CressemExtractLibrary.Data.Odb.Feature;

namespace CressemExtractLibrary.Data.Gerber
{
	internal class GerberData : ExtractData
	{
		private GerberData() : base() { }

		public GerberData(string loadPath, string savePath) : base(loadPath, savePath)
		{
		}

		public override string[] GetStepNames()
		{
			throw new System.NotImplementedException();
		}

		public override string[] GetLayerNames(string stepName)
		{
			throw new System.NotImplementedException();
		}

		public override RectangleF GetStepRoi(string stepName)
		{
			throw new System.NotImplementedException();
		}

		public override OdbFeatureBase[] GetFeatures(string stepName, string layerName)
		{
			throw new System.NotImplementedException();
		}
	}
}
