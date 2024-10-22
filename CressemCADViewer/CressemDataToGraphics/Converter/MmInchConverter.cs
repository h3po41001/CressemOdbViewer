using System.Drawing;
using System;

namespace CressemDataToGraphics.Converter
{
	public static class MmInchConverter
	{
		private static readonly double MM_TO_INCH = 25.4;
		private static readonly int DIGITS = 10;

		// 1 mil = 1/1000 inch
		// 1 inch = 2.54cm = 25.4mm
		// 1 mil = 1/1000 * 25.4 mm

		public static double ConvertInchToMM(this double inch)
		{
			return Math.Round(inch * MM_TO_INCH, DIGITS);
		}

		public static double ConvertInchToUM(this double inch)
		{
			return Math.Round(inch * MM_TO_INCH / 1000, DIGITS);
		}

		public static double ConvertMMToInch(this double mm)
		{
			return Math.Round(mm / MM_TO_INCH, DIGITS);
		}

		public static double ConvertUMToInch(this double mm)
		{
			return Math.Round(mm * 1000 / MM_TO_INCH, DIGITS);
		}

		public static RectangleF ConvertInchToMM(this RectangleF inch)
		{
			return new RectangleF(
				(float)ConvertInchToMM(inch.X),
				(float)ConvertInchToMM(inch.Y),
				(float)ConvertInchToUM(inch.Width),
				(float)ConvertInchToUM(inch.Height));
		}

		public static RectangleF ConvertMMToInch(this RectangleF mil)
		{
			return new RectangleF(
				(float)ConvertMMToInch(mil.X),
				(float)ConvertMMToInch(mil.Y),
				(float)ConvertUMToInch(mil.Width),
				(float)ConvertUMToInch(mil.Height));
		}
	}
}
