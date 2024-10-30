using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ImageControl.Extension;
using SharpDX.Direct2D1;

namespace ImageControl.Shape.DirectX
{
	internal class DirectSurface : DirectShape
	{
		private DirectSurface() : base() { }

		public DirectSurface(bool isPositive,
			IEnumerable<DirectShape> polygons,
			Factory factory, RenderTarget render, Color color) : base(isPositive, factory, render, color)
		{
			Polygons = new List<DirectShape>(polygons);
			SetShape();
		}

		public IEnumerable<DirectShape> Polygons { get; private set; }

		public override void SetShape()
		{
			try
			{
				ShapeGemotry = new GeometryGroup(Factory, FillMode.Alternate,
					Polygons.Select(p => p.ShapeGemotry).ToArray());
			}
			catch (System.Exception)
			{
				ShapeGemotry.Dispose();
				throw;
			}
		}

		public override RectangleF GetBounds()
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

		public override void Fill(RenderTarget render, bool isHole)
		{
			if (IsPositive)
			{
				render.FillGeometry(ShapeGemotry, DefaultBrush);
			}
			else
			{
				render.FillGeometry(ShapeGemotry, HoleBrush);
			}
		}
	}
}
