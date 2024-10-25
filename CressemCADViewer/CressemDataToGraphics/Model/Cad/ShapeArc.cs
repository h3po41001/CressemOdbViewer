using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CressemDataToGraphics.Model.Graphics.DirectX;

namespace CressemDataToGraphics.Model.Cad
{
	internal class ShapeArc : ShapeBase
	{
		private ShapeArc() { }

		public ShapeArc(float pixelResolution,
			float sx, float sy,
			float ex, float ey, 
			float cx, float cy, float width,
			float radius, float startAngle, float endAngle) : base(cx, cy)
		{
			Sx = sx * pixelResolution;
			Sy = sy * pixelResolution;
			Ex = ex * pixelResolution;
			Ey = ey * pixelResolution;
			Width = width * pixelResolution;
			Radius = radius;
			StartAngle = startAngle;
			EndAngle = endAngle;
		}

		public float Sx { get; private set; }

		public float Sy { get; private set; }

		public float Ex { get; private set; }

		public float Ey { get; private set; }

		public float Width { get; private set; }

		public float Radius { get; private set; }

		public float StartAngle { get; private set; }

		public float EndAngle { get; private set; }
	}
}
