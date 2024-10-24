﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CressemExtractLibrary.Data.Odb.Attribute
{
	internal class OdbAttribute
	{
		private OdbAttribute() { }

		public OdbAttribute(string name, string value)
		{
			Name = name;
			Value = value;
		}

		public string Name { get; private set; }

		public string Value { get; private set; }
	}
}
