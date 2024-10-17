using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

			if (int.TryParse(split[0], out int index) is false)
			{
				return null;
			}

			return new OdbAttributeTextString(index, split[1]);
		}
	}
}
