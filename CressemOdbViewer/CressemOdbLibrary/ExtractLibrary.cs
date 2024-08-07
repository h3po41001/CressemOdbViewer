using CressemExtractLibrary.Extractor;
using CressemExtractLibrary.Extractor.Gerber;
using CressemExtractLibrary.Extractor.Odb;
using CressemExtractLibrary.Data;

namespace CressemExtractLibrary
{
	public class ExtractLibrary
	{
		private static ExtractLibrary _instance;
		private DataFormat _dataFormat;
		private IDataExtractor _dataExtractor;

		private ExtractLibrary() { }

		public static ExtractLibrary Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new ExtractLibrary();
				}

				return _instance;
			}
		}

		public bool SetDataFormat(DataFormat dataFormatType)
		{
			_dataFormat = dataFormatType;

			switch (_dataFormat)
			{
				case DataFormat.Gerber:
					_dataExtractor = new GerberExtractor();
					break;
				case DataFormat.Odb:
					_dataExtractor = new OdbExtractor();
					break;
				default:
					return false;
			}

			return true;
		}

		public bool ExtractData(ExtractData data)
		{
			return _dataExtractor.ExtractData(data);
		}
	}
}
