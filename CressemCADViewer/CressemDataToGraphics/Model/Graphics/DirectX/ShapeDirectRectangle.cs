using CressemDataToGraphics.Factory;
using ImageControl.Shape.DirectX.Interface;

namespace CressemDataToGraphics.Model.Graphics.DirectX
{
	internal class ShapeDirectRectangle : ShapeDirectBase, IDirectRectangle
	{
		private ShapeDirectRectangle() : base()
		{
		}

		public ShapeDirectRectangle(float left, float top,
			float right, float bottom) : base()
		{
			Left = left;
			Top = top;
			Right = right;
			Bottom = bottom;
		}

		public float Left { get; private set; }

		public float Top { get; private set; }

		public float Right { get; private set; }

		public float Bottom { get; private set; }

		public static ShapeDirectRectangle Create(bool useMM,
			float pixelResolution, bool isMM,
			double xDatum, double yDatum, double cx, double cy,
			int orient, bool isMirrorXAxis,
			double width, double height)
		{
			var rect = ShapeFactory.Instance.CreateRectangle(useMM, pixelResolution, isMM,
				xDatum, yDatum, cx, cy, orient, isMirrorXAxis, width, height);

			return new ShapeDirectRectangle(rect.Sx, -rect.Sy,
				rect.Sx + rect.Width, -rect.Sy + rect.Height);
		}
	}
}
