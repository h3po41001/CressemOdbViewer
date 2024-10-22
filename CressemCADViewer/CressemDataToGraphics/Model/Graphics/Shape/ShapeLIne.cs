using CressemDataToGraphics.Converter;
using CressemExtractLibrary.Data.Interface.Features;
using ImageControl.Shape.Interface;

namespace CressemDataToGraphics.Model.Graphics.Shape
{
	internal class ShapeLine : ShapeBase, IShapeLine
	{
		private ShapeLine() { }

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

		public static ShapeLine CreateGdiPlus(bool useMM, float pixelResolution,
			IFeatureLine line)
		{
			double sx = line.X;
			double sy = line.Y;
			double ex = line.Ex;
			double ey = line.Ey;

			if (useMM is true)
			{
				if (line.IsMM is false)
				{
					sx = sx.ConvertInchToMM();
					sy = sy.ConvertInchToMM();
					ex = ex.ConvertInchToMM();
					ey = ey.ConvertInchToMM();
				}
			}
			else
			{
				if (line.IsMM is true)
				{
					sx = sx.ConvertMMToInch();
					sy = sy.ConvertMMToInch();
					ex = ex.ConvertMMToInch();
					ey = ey.ConvertMMToInch();
				}
			}

			return new ShapeLine(pixelResolution,
				(float)sx, (float)-sy,
				(float)ex, (float)-ey);
		}

		public static ShapeLine CreateOpenGl(bool useMM, float pixelResolution,
			IFeatureLine line)
		{
			throw new System.NotImplementedException();
		}
	}
}
