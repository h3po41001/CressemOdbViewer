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

		// 1 mil = 1/1000 inch
		// 1 inch = 2.54cm = 25.4mm
		// 1 mil = 1/1000 * 25.4 mm

		public double ConvertInchToMM(double inch)
		{
			return inch * 25.4;
		}

		public double ConvertMMToInch(double mm)
		{
			return mm / 25.4;
		}
	}
}
