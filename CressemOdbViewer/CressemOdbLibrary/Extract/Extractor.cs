using CressemExtractLibrary.Data;

namespace CressemExtractLibrary.Extract
{
	internal abstract class Extractor
	{
		private ExtractData _data;

		protected Extractor() { }

		public ExtractData ExtractData
		{
			get => _data;
			protected set => _data = value;
		}

		public abstract bool Extract();
	}
}
