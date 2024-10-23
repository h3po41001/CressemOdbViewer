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

		public static PointF Rotate(this PointF point, PointF center, float angle, bool isMirrorXAxis)
		{
			float radian = angle * (float)(Math.PI / 180);
			float cosTheta = (float)Math.Cos(radian);
			float sinTheta = (float)Math.Sin(radian);

			float dx = point.X - center.X;
			float dy = point.Y - center.Y;

			float rotatedX = dx * cosTheta - dy * sinTheta;
			float rotatedY = dx * sinTheta + dy * cosTheta;

			if (isMirrorXAxis)
			{
				rotatedX = -rotatedX;
			}

			return new PointF(rotatedX + center.X, rotatedY + center.Y);
		}
	}
}
