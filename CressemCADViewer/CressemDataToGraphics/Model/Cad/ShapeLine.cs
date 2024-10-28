using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CressemDataToGraphics.Model.Cad
{
	internal class ShapeLine : ShapeBase
	{
		private ShapeLine() { }

		public ShapeLine(float pixelResolution,
			float cx, float cy,
			float sx, float sy,
			float ex, float ey, float width = 0) : base(cx, cy)
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
	}
}
