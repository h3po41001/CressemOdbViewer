using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CressemExtractLibrary.Data.Odb;

namespace CressemExtractLibrary.Convert
{
	internal class Converter
	{
		private static Converter _convert;

		private Converter()
		{

		}

		public static Converter Instance
		{
			get
			{
				if (_convert is null)
				{
					_convert = new Converter();
				}

				return _convert;
			}
		}

		public YesOrNo ConvertToYesOrNo(string value)
		{
			return value.ToLower() == "yes" ? YesOrNo.Yes : YesOrNo.No;
		}
	}
}
