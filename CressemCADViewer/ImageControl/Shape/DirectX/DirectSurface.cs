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

				SetBounds();
			}
			catch (System.Exception)
			{
				ShapeGemotry?.Dispose();
				throw;
			}
		}

		public void SetBounds()
		{
			if (Polygons is null)
			{
				return;
			}

			List<RectangleF> bounds = new List<RectangleF>();
			foreach (DirectShape shape in Polygons)
			{
				if (shape is DirectPolygon polygon)
				{
					bounds.Add(polygon.Bounds);
				}
			}

			Bounds = bounds.GetBounds();
		}

		public override void Draw(RenderTarget render, RectangleF roi)
		{
			if (roi.IntersectsWith(Bounds) is true)
			{
				foreach (var polygon in Polygons)
				{
					polygon.Draw(render, roi);
				}
			}
		}

		public override void Fill(RenderTarget render, bool isHole, RectangleF roi)
		{
			if (ShapeGemotry is null)
			{
				return;
			}

			//if (roi.IntersectsWith(Bounds) is true)
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
}
