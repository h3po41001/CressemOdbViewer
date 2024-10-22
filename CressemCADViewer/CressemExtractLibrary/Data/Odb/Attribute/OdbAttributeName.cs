namespace CressemExtractLibrary.Data.Odb.Attribute
{
	internal class OdbAttributeName
	{
		private OdbAttributeName() { }

		public OdbAttributeName(int index, string name)
		{
			Index = index;
			Name = name;
		}

		public int Index { get; private set; }

		public string Name { get; private set; }

		public static OdbAttributeName Create(string param)
		{
			string[] split = param.Split(' ');
			if (split.Length != 2)
			{
				return null;
			}

			string indexString = split[0].Substring(1);
			if (int.TryParse(indexString, out int index) is false)
			{
				return null;
			}

			return new OdbAttributeName(index, split[1]);
		}
	}
}
