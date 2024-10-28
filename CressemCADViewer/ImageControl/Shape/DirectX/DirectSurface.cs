using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageControl.Extension;
using SharpDX.Direct2D1;

namespace ImageControl.Shape.DirectX
{
	internal class DirectSurface : DirectShape
	{
		private DirectSurface() : base() { }

		public DirectSurface(IEnumerable<DirectShape> polygons,
			Factory factory, RenderTarget render, Color color) : base(factory, render, color)
		{
			Polygons = new List<DirectShape>(polygons);
		}

		public IEnumerable<DirectShape> Polygons { get; private set; }

		public override void SetShape()
		{
			throw new NotImplementedException();
		}

		public RectangleF GetBounds()
		{
			if (Polygons is null)
			{
				return RectangleF.Empty;
			}

			List<RectangleF> bounds = new List<RectangleF>();
			foreach (DirectShape shape in Polygons)
			{
				if (shape is DirectPolygon polygon)
				{
					bounds.Add(polygon.GetBounds());
				}
			}

			return bounds.GetBounds();
		}

		public override void Draw(RenderTarget render)
		{
			foreach (var polygon in Polygons)
			{
				polygon.Draw(render);
			}
		}

		public override void Fill(RenderTarget render)
		{
			foreach (var polygon in Polygons)
			{
				polygon.Fill(render);
			}
		}
	}
}
