using System.Drawing;

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
	}
}
