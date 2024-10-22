using System.Drawing;
using CressemExtractLibrary.Data.Interface.Features;
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

		public override IFeatureBase GetStepRoi(string stepName)
		{
			throw new System.NotImplementedException();
		}

		public override IFeatureBase[] GetFeatures(string stepName, string layerName,
			out double xDatum, out double y)
		{
			throw new System.NotImplementedException();
		}
	}
}
