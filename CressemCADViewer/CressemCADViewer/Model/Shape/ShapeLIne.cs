using CressemExtractLibrary.Data.Interface.Features;
using ImageControl.Shape.Interface;

namespace CressemCADViewer.Model.Shape
{
	internal class ShapeLine : ShapeBase, IShapeLine
	{
		public ShapeLine(float pixelResolution,
			float sx, float sy,
			float ex, float ey) : base(pixelResolution)
		{
			Sx = sx;
			Sy = sy;
			Ex = ex;
			Ey = ey;
		}

		public float Sx { get; private set; }

		public float Sy { get; private set; }

		public float Ex { get; private set; }

		public float Ey { get; private set; }

		public static IShapeLine Create(float pixelResolution,
			IFeatureLine line)
		{
			return new ShapeLine(pixelResolution,
				(float)line.X, (float)line.Y,
				(float)line.Ex, (float)line.Ey);
		}
	}
}
