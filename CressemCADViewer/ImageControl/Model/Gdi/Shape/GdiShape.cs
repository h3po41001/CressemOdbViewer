using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageControl.Model.Gdi.Shape
{
	public abstract class GdiShape
	{
		protected GdiShape() { }

		protected GdiShape(float pixelResolution)
		{
			PixelResolution = pixelResolution;
		}

		protected float PixelResolution { get; private set; }

		public abstract void Draw(Graphics graphics, Pen pen);

		public abstract void AddPath(GraphicsPath path, Pen pen);
	}
}
