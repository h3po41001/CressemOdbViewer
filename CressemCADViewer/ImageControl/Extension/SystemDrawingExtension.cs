using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageControl.Extension
{
	public static class SystemDrawingExtension
	{
		public static Point GetCenter(this Rectangle rect)
		{
			return new Point((rect.Right - rect.Left) / 2, (rect.Bottom - rect.Top) / 2);
		}

		public static PointF GetCenterF(this Rectangle rect)
		{
			return new PointF((rect.Right - rect.Left) * 0.5f, (rect.Bottom - rect.Top) * 0.5f);
		}

		public static Point GetCenter(this RectangleF rect)
		{
			return new Point((int)((rect.Right - rect.Left) / 2), (int)((rect.Bottom - rect.Top) / 2));
		}

		public static PointF GetCenterF(this RectangleF rect)
		{
			return new PointF((rect.Right - rect.Left) * 0.5f, (rect.Bottom - rect.Top) * 0.5f);
		}
	}
}
