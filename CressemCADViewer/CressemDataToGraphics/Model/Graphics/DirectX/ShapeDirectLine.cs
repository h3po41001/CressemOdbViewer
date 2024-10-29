using System.Drawing;
using CressemDataToGraphics.Converter;
using CressemDataToGraphics.Factory;
using ImageControl.Extension;
using ImageControl.Shape.DirectX.Interface;

namespace CressemDataToGraphics.Model.Graphics.DirectX
{
	internal class ShapeDirectLine : ShapeDirectBase, IDirectLine
	{
		private ShapeDirectLine() { }

		public ShapeDirectLine(
			float sx, float sy,
			float ex, float ey,
			float width = 0) : base()
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
			var shapeLine = ShapeFactory.Instance.CreateLine(useMM, 
				pixelResolution, isMM, 
				xDatum, yDatum, cx, cy,
				orient, isMirrorXAxis, 
				sx, sy, ex, ey, width);

			return new ShapeDirectLine(shapeLine.Sx, -shapeLine.Sy,
				shapeLine.Ex, -shapeLine.Ey, shapeLine.LineWidth);
		}
	}
}
