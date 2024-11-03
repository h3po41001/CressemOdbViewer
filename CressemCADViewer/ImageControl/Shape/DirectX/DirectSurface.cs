using System;
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
			Factory factory, RenderTarget render, Color color,
			float skipRatio) : base(isPositive, factory, render, color)
		{
			Polygons = new List<DirectShape>(polygons);
			SetShape(skipRatio);
		}

		public IEnumerable<DirectShape> Polygons { get; private set; }

		public override void SetShape(float skipRatio)
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

				SetBounds(skipRatio);
			}
			catch (System.Exception)
			{
				ShapeGemotry?.Dispose();
				throw;
			}
		}

		public void SetBounds(float skipRatio)
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
			SkipSize = new SizeF(
				Math.Abs(Bounds.Width * skipRatio),
				Math.Abs(Bounds.Height * skipRatio));
		}

		public override void Draw(RenderTarget render)
		{
			foreach (var polygon in Polygons)
			{
				polygon.Draw(render);
			}
		}

		public override void Fill(RenderTarget render,
			bool isHole, RectangleF roi)
		{
			if (ShapeGemotry is null)
			{
				return;
			}

			// 확대한 shape 크기가 roi 보다 커야됨. (작지 않아서 그려도 되는것)
			if (SkipSize.Width >= roi.Width &&
				SkipSize.Height >= roi.Height)
			{
				if (roi.IntersectsWith(Bounds) is true)
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
}
