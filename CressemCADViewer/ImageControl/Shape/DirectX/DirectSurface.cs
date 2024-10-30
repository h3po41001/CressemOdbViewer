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
				var geometries = new List<Geometry>();
				foreach (var polygon in Polygons)
				{
					if (polygon is DirectPolygon directPolygon)
					{
						if (directPolygon.ShapeGemotry is null)
						{
							continue;
						}

						geometries.Add(directPolygon.ShapeGemotry);
					}
				}

				if (geometries.Any() is true)
				{
					ShapeGemotry = new GeometryGroup(Factory, FillMode.Alternate, geometries.ToArray());
				}
				else
				{
					ShapeGemotry = null;
				}
			}
			catch (System.Exception)
			{
				ShapeGemotry?.Dispose();
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
			if (ShapeGemotry is null)
			{
				return;
			}

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
