namespace CressemExtractLibrary.Data.Odb.Feature
{
	internal class OdbFeatureBase
	{
		protected OdbFeatureBase() { }

		public OdbFeatureBase(int index, bool isMM, 
			string polarity, string decode)
		{
			Index = index;
			IsMM = isMM;
			Polarity = polarity;
			Decode = decode;
		}

		public int Index { get; private set; }

		public bool IsMM { get; private set; }

		public string Polarity { get; private set; }

		// gerber decode
		public string Decode { get; private set; }
	}
}
