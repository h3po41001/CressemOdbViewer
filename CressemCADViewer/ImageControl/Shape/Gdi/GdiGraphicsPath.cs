using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageControl.Model.Shape.Gdi;

namespace ImageControl.Shape.Gdi
{
	internal class GdiGraphicsPath
	{
		private GdiGraphicsPath() { }

		public GdiGraphicsPath(float xdatum, float ydatum, int orient, 
			IEnumerable<GdiShape> shapes)
		{
			Xdatum = xdatum;
			Ydatum = ydatum;
			Orient = orient;
			Shapes = new List<GdiShape>(shapes);
		}

		public float Xdatum { get; private set; }

		public float Ydatum { get; private set; }

		public int Orient { get; private set; }

		public List<GdiShape> Shapes { get; private set; }
	}
}
