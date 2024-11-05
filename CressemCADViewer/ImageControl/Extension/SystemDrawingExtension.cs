using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ImageControl.Extension
{
	public static class SystemDrawingExtension
	{
		public static Point GetCenter(this Rectangle rect)
		{
			return new Point((rect.Right + rect.Left) / 2, (rect.Bottom + rect.Top) / 2);
		}

		public static PointF GetCenterF(this Rectangle rect)
		{
			return new PointF((rect.Right + rect.Left) * 0.5f, (rect.Bottom + rect.Top) * 0.5f);
		}

		public static Point GetCenter(this RectangleF rect)
		{
			return new Point((int)((rect.Right + rect.Left) / 2), (int)((rect.Bottom + rect.Top) / 2));
		}

		public static PointF GetCenterF(this RectangleF rect)
		{
			return new PointF((rect.Right + rect.Left) * 0.5f, (rect.Bottom + rect.Top) * 0.5f);
		}

		public static RectangleF GetBounds(this IEnumerable<RectangleF> rectangles)
		{
			float left = rectangles.Min(x => x.Left);
			float top = rectangles.Min(x => x.Top);
			float right = rectangles.Max(x => x.Right);
			float bottom = rectangles.Max(x => x.Bottom);

			return new RectangleF(left, top, right - left, bottom - top);
		}

		public static PointF Offset(this PointF point, float dx, float dy)
		{
			return new PointF(point.X + dx, point.Y + dy);
		}

		public static PointF Rotate(this PointF point, PointF center, int angle, bool isFlipHorizontal)
		{
			double radian = angle * (Math.PI / 180);
			double cosTheta = Math.Cos(radian);
			double sinTheta = Math.Sin(radian);

			double dx = point.X - center.X;
			double dy = point.Y - center.Y;

			double rotatedX = dx * cosTheta - dy * sinTheta;
			double rotatedY = dx * sinTheta + dy * cosTheta;

			if (isFlipHorizontal)
			{
				rotatedX = -rotatedX;
			}

			return new PointF((float)(rotatedX + center.X), (float)(rotatedY + center.Y));
		}

		public static Point Rotate(this Point point, Point center, int angle, bool isFlipHorizontal)
		{
			double radian = angle * (Math.PI / 180);
			double cosTheta = Math.Cos(radian);
			double sinTheta = Math.Sin(radian);

			double dx = point.X - center.X;
			double dy = point.Y - center.Y;

			double rotatedX = dx * cosTheta - dy * sinTheta;
			double rotatedY = dx * sinTheta + dy * cosTheta;

			if (isFlipHorizontal)
			{
				rotatedX = -rotatedX;
			}

			return new Point((int)Math.Round(rotatedX + center.X), (int)Math.Round(rotatedY + center.Y));
		}
	}
}
