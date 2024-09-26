using System.IO;
using CressemExtractLibrary.Data.Odb.Matrix;

namespace CressemExtractLibrary.Data.Odb.Layer
{
	internal class OdbLayerLoader
	{
		private static OdbLayerLoader _instance;



		public static OdbLayerLoader Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new OdbLayerLoader();
				}
				return _instance;
			}
		}

		public OdbLayer Load(string dirPath, OdbMatrixLayer matrixLayer)
		{
			if (File.Exists(dirPath) is false)
			{
				return null;
			}

			//string symbolsFolderPath = Path.Combine(dirPath, symbolsFolderPath);

			return null;
		}
	}
}
