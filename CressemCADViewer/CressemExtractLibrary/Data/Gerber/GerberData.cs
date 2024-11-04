using CressemExtractLibrary.Data.Interface.Features;
using CressemExtractLibrary.Data.Interface.Step;

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

		public override IStepHeader GetStepHeader(string stepName)
		{
			throw new System.NotImplementedException();
		}

		public override IFeatureBase GetStepProfile(string stepName)
		{
			throw new System.NotImplementedException();
		}

		public override IFeatureBase[] GetFeatures(string stepName, string layerName)
		{
			throw new System.NotImplementedException();
		}
	}
}
