using System.Drawing;
using CressemDataToGraphics.Converter;
using CressemDataToGraphics.Factory;
using ImageControl.Extension;
using ImageControl.Shape.DirectX.Interface;

namespace CressemDataToGraphics.Model.Graphics.DirectX
{
	internal class ShapeDirectEllipse : ShapeDirectBase, IDirectEllipse
	{
		private ShapeDirectEllipse() : base()
		{
		}

		public ShapeDirectEllipse(float sx, float sy,
			float width, float height) : base()
		{
			Sx = sx;
			Sy = sy;
			Width = width;
			Height = height;
		}

		public float Sx { get; private set; }

		public float Sy { get; private set; }

		public float Width { get; private set; }

		public float Height { get; private set; }

		public static ShapeDirectEllipse Create(bool useMM,
			float pixelResolution, bool isMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis,
			double width, double height)
		{
			var shapeEllipse = ShapeFactory.Instance.CreateEllipse(useMM, pixelResolution, isMM,
				xDatum, yDatum, cx, cy, orient, isMirrorXAxis, width, height);

			// Graphics는 y좌표가 반대이므로 -1곱한다
			return new ShapeDirectEllipse(shapeEllipse.Sx, -shapeEllipse.Sy,
				shapeEllipse.Width, shapeEllipse.Height);
		}
	}
}
