namespace CressemExtractLibrary.Data.Odb.Feature
{
	internal class OdbFeatures
	{
		protected OdbFeatures()
		{
		}

		public OdbFeatures(bool isMM)
		{
			IsMM = isMM;
		}

		public bool IsMM { get; private set; }
	}
}
