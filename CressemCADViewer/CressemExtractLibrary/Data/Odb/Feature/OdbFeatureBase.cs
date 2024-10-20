namespace CressemExtractLibrary.Data.Odb.Feature
{
	public class OdbFeatureBase
	{
		protected OdbFeatureBase() { }

		public OdbFeatureBase(int index, bool isMM, double x, double y,
			string polarity, string decode)
		{
			Index = index;
			IsMM = isMM;
			X = x;
			Y = y;
			Polarity = polarity;
			Decode = decode;
		}

		public int Index { get; private set; }

		public bool IsMM { get; private set; }

		public double X { get; private set; }

		public double Y { get; private set; }

		public string Polarity { get; private set; }

		// gerber decode
		public string Decode { get; private set; }
	}
}
