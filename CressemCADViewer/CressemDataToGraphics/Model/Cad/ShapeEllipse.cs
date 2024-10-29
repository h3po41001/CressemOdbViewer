namespace CressemDataToGraphics.Model.Cad
{
	internal class ShapeEllipse : ShapeBase
	{
		public ShapeEllipse(float pixelResolution,
			float cx, float cy,
			float sx, float sy,
			float width, float height) : base(cx * pixelResolution, cy * pixelResolution)
		{
			Sx = sx * pixelResolution;
			Sy = sy * pixelResolution;
			Width = width * pixelResolution;
			Height = height * pixelResolution;
		}

		public float Sx { get; private set; }

		public float Sy { get; private set; }

		public float Width { get; private set; }

		public float Height { get; private set; }
	}
}
