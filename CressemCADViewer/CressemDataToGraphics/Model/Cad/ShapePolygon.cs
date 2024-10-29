using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CressemDataToGraphics.Model.Cad
{
	internal class ShapePolygon : ShapeBase
	{
		private ShapePolygon() { }

		public ShapePolygon(float pixelResolution,
			float cx, float cy,
			bool isFill,
			IEnumerable<PointF> points) : base(cx * pixelResolution, cy * pixelResolution)
		{
			IsFill = isFill;
			Points = new List<PointF>(points.Select(
					x => new PointF(x.X * pixelResolution, x.Y * pixelResolution)));
		}

		public bool IsFill { get; private set; }

		public IEnumerable<PointF> Points { get; private set; }
	}
}
