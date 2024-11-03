using CressemDataToGraphics.Factory;
using ImageControl.Shape.DirectX.Interface;

namespace CressemDataToGraphics.Model.Graphics.DirectX
{
	internal class ShapeDirectRectangle : ShapeGraphicsBase, IDirectRectangle
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
			double datumX, double datumY,
			double cx, double cy,
			int orient, bool isFlipHorizontal,
			double width, double height)
		{
			var rect = ShapeFactory.Instance.CreateRectangle(useMM, 
				pixelResolution, isMM,
				datumX, datumY, cx, cy, 
				orient, isFlipHorizontal, width, height);

			return new ShapeDirectRectangle(rect.Sx, -rect.Sy,
				rect.Sx + rect.Width, -rect.Sy + rect.Height);
		}
	}
}
