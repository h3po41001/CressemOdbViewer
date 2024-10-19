using System.Drawing;

namespace CressemExtractLibrary.Convert
{
	public class Converter
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

		public RectangleF ConvertInchToMM(RectangleF inch)
		{
			return new RectangleF(
				(float)ConvertInchToMM(inch.X), 
				(float)ConvertInchToMM(inch.Y), 
				(float)ConvertInchToMM(inch.Width), 
				(float)ConvertInchToMM(inch.Height));
		}

		public RectangleF ConvertMMToInch(RectangleF mil)
		{
			return new RectangleF(
				(float)ConvertMMToInch(mil.X),
				(float)ConvertMMToInch(mil.Y),
				(float)ConvertMMToInch(mil.Width),
				(float)ConvertMMToInch(mil.Height));
		}
	}
}
