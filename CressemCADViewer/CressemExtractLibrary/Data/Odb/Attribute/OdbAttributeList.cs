using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CressemExtractLibrary.Data.Odb.Attribute
{
	internal class OdbAttributeList
	{
		private OdbAttributeList() { }

		public OdbAttributeList(IEnumerable<OdbAttributeName> names,
			IEnumerable<OdbAttributeTextString> texts)
		{
			Names = names.ToDictionary(x => x.Index);
			Texts = texts.ToDictionary(x => x.Index);
		}

		public Dictionary<int, OdbAttributeName> Names { get; private set; } 

		public Dictionary<int, OdbAttributeTextString> Texts { get; private set; }
	}
}
