﻿using System.Drawing;
using System;
using System.Collections;
using System.Collections.Generic;

namespace CressemDataToGraphics.Converter
{
	public static class DataConverter
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

		public static float ConvertInchToMM(this float inch)
		{
			return (float)Math.Round(inch * MM_TO_INCH, DIGITS);
		}

		public static double ConvertMilInchToMM(this double inch)
		{
			return Math.Round(inch * MM_TO_INCH / 1000, DIGITS);
		}
		public static float ConvertInchToUM(this float inch)
		{
			return (float)Math.Round(inch * MM_TO_INCH / 1000, DIGITS);
		}

		public static IEnumerable<PointF> ConvertInchToMM(this IEnumerable<PointF> inch)
		{
			foreach (var point in inch)
			{
				yield return new PointF(
					(float)ConvertInchToMM(point.X),
					(float)ConvertInchToMM(point.Y));
			}
		}

		public static double ConvertMMToInch(this double mm)
		{
			return Math.Round(mm / MM_TO_INCH, DIGITS);
		}

		public static float ConvertMMToInch(this float mm)
		{
			return (float)Math.Round(mm / MM_TO_INCH, DIGITS);
		}

		public static double ConvertUMToInch(this double mm)
		{
			return Math.Round(mm * 1000 / MM_TO_INCH, DIGITS);
		}

		public static float ConvertUMToInch(this float mm)
		{
			return (float)Math.Round(mm * 1000 / MM_TO_INCH, DIGITS);
		}

		public static IEnumerable<PointF> ConvertMMToInch(this IEnumerable<PointF> mil)
		{
			foreach (var point in mil)
			{
				yield return new PointF(
					(float)ConvertMMToInch(point.X),
					(float)ConvertMMToInch(point.Y));
			}
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

		public static double ConvertNormalizeAngle(double angle)
		{
			double normalize = angle % 360;
			if (normalize <= 0)
			{
				normalize += 360;
			}

			return normalize;
		}
	}
}
