namespace CressemExtractLibrary.Data.Odb.Attribute
{
	internal class OdbAttributeTextString
	{
		private OdbAttributeTextString() { }

		public OdbAttributeTextString(int index, string text)
		{
			Index = index;
			Text = text;
		}

		public int Index { get; private set; }

		public string Text { get; private set; }

		public static OdbAttributeTextString Create(string param)
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

			return new OdbAttributeTextString(index, split[1]);
		}
	}
}
