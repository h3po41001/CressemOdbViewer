namespace CressemExtractLibrary.Data.Odb.Feature
{
	internal class OdbFeatureBase
	{
		protected OdbFeatureBase() { }

		public OdbFeatureBase(bool isMM, string polarity, string decode)
		{
			IsMM = isMM;
			Polarity = polarity;
			Decode = decode;
		}

		public bool IsMM { get; private set; }

		public string Polarity { get; private set; }

		// gerber decode
		public string Decode { get; private set; }
	}
}
