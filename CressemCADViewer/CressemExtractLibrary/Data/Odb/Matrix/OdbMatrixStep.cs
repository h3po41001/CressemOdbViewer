namespace CressemExtractLibrary.Data.Odb.Matrix
{
	internal class OdbMatrixStep
	{
		public OdbMatrixStep(int col, string name)
		{
			Column = col;
			Name = name;
		}

		public int Column { get; private set; }

		public string Name { get; private set; }
	}
}
