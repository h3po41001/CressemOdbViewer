using System.Drawing;

namespace CressemDataToGraphics.Converter
{
	public static class MmInchConverter
	{
		// 1 mil = 1/1000 inch
		// 1 inch = 2.54cm = 25.4mm
		// 1 mil = 1/1000 * 25.4 mm

		public static double ConvertInchToMM(this double inch)
		{
			return inch * 25.4;
		}

		public static double ConvertMMToInch(this double mm)
		{
			return mm / 25.4;
		}

		public static RectangleF ConvertInchToMM(this RectangleF inch)
		{
			return new RectangleF(
				(float)ConvertInchToMM(inch.X),
				(float)ConvertInchToMM(inch.Y),
				(float)ConvertInchToMM(inch.Width),
				(float)ConvertInchToMM(inch.Height));
		}

		public static RectangleF ConvertMMToInch(this RectangleF mil)
		{
			return new RectangleF(
				(float)ConvertMMToInch(mil.X),
				(float)ConvertMMToInch(mil.Y),
				(float)ConvertMMToInch(mil.Width),
				(float)ConvertMMToInch(mil.Height));
		}
	}
}
