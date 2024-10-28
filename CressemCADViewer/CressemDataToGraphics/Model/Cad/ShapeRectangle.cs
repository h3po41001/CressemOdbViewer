using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CressemDataToGraphics.Model.Cad
{
	internal class ShapeRectangle : ShapeBase
	{
		private ShapeRectangle() { }

		public ShapeRectangle(float pixelResolution,
			float cx, float cy,
			float sx, float sy, float width, float height) : base(cx, cy)
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
