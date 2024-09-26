using System.Drawing;

namespace CressemExtractLibrary.Data.Odb.Matrix
{
	internal class OdbMatrixLayer
	{
		private OdbMatrixLayer() { }

		public OdbMatrixLayer(int row, string context, string type,
			string name, string oldName, bool polarity,
			string startName, string endName, string addName)
		{
			Row = row;
			Context = context;
			Type = type;
			Name = name;
			OldName = oldName;
			Polarity = polarity;
			StartName = startName;
			EndName = endName;
			AddName = addName;
		}

		public int Row { get; private set; } = 0;

		public string Context { get; private set; }

		public string Type { get; private set; }

		public string Name { get; private set; }

		public string OldName { get; private set; }

		public bool Polarity { get; private set; }

		public string StartName { get; private set; }

		public string EndName { get; private set; }

		public string AddName { get; private set; }
	}
}
