namespace CressemExtractLibrary.Data
{
	public class ExtractData
	{
		private string _loadPath = string.Empty;
		private string _savePath = string.Empty;

		protected ExtractData() { }

		public ExtractData(string loadPath, string savePath)
		{
			_loadPath = loadPath;
			_savePath = savePath;
		}

		public void LoadData()
		{
			// TODO: Implement the logic to load data from the specified load path
		}

		public void SaveData()
		{
			// TODO: Implement the logic to save data to the specified save path
		}
	}
}
