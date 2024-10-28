using System.Drawing;
using CressemDataToGraphics.Converter;
using ImageControl.Extension;
using ImageControl.Shape.DirectX.Interface;

namespace CressemDataToGraphics.Model.Graphics.DirectX
{
	internal class ShapeDirectLine : ShapeDirectBase, IDirectLine
	{
		private ShapeDirectLine() { }

		public ShapeDirectLine(float pixelResolution,
			float sx, float sy,
			float ex, float ey,
			float width = 0) : base(pixelResolution)
		{
			Sx = sx;
			Sy = sy;
			Ex = ex;
			Ey = ey;
			LineWidth = width;
		}

		public float Sx { get; private set; }

		public float Sy { get; private set; }

		public float Ex { get; private set; }

		public float Ey { get; private set; }

		public float LineWidth { get; private set; }

		public static ShapeDirectLine Create(bool useMM,
			float pixelResolution, bool isMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis,
			double sx, double sy, double ex, double ey, double width)
		{
			float fsx = (float)sx;
			float fsy = (float)sy;
			float fex = (float)ex;
			float fey = (float)ey;
			float fwidth = (float)width;

			if (useMM is true)
			{
				if (isMM is false)
				{
					fsx = (float)sx.ConvertInchToMM();
					fsy = (float)sy.ConvertInchToMM();
					fex = (float)ex.ConvertInchToMM();
					fey = (float)ey.ConvertInchToMM();
					fwidth = (float)width.ConvertInchToUM();
				}
			}
			else
			{
				if (isMM is true)
				{
					fsx = (float)sx.ConvertMMToInch();
					fsy = (float)sy.ConvertMMToInch();
					fex = (float)ex.ConvertMMToInch();
					fey = (float)ey.ConvertMMToInch();
					fwidth = (float)width.ConvertUMToInch();
				}
			}

			PointF start = new PointF(fsx, fsy);
			PointF end = new PointF(fex, fey);

			if (orient > 0)
			{
				PointF datum = new PointF((float)(xDatum + cx), (float)(yDatum + cx));

				start = start.Rotate(datum, orient, isMirrorXAxis);
				end = end.Rotate(datum, orient, isMirrorXAxis);
			}

			return new ShapeDirectLine(pixelResolution,
				start.X, -start.Y, end.X, -end.Y, fwidth);
		}
	}
}
