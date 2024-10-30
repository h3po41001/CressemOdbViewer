using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ImageControl.Extension;
using SharpDX.Direct2D1;

namespace ImageControl.Shape.DirectX
{
	internal class DirectSurfaces : DirectShape
	{
		private DirectSurfaces() : base() { }

		public DirectSurfaces(bool isPositive,
			IEnumerable<DirectSurface> shapes,
			Factory factory, RenderTarget render, Color color) : base(isPositive, factory, render, color)
		{
			Surfaces = new List<DirectSurface>(shapes);
			SetShape();
		}

		public IEnumerable<DirectSurface> Surfaces { get; private set; }

		public override void SetShape()
		{
			try
			{
				//ShapeGemotry = new GeometryGroup(Factory, FillMode.Winding,
				//	Surfaces.SelectMany(s => s.Polygons.Select(p => p.ShapeGemotry)).ToArray());
			}
			catch (System.Exception)
			{
				ShapeGemotry.Dispose();
				throw;
			}
		}

		public override RectangleF GetBounds()
		{
			if (Surfaces is null)
			{
				return RectangleF.Empty;
			}

			List<RectangleF> bounds = new List<RectangleF>();
			foreach (DirectShape surface in Surfaces)
			{
				bounds.Add(surface.GetBounds());
			}

			return bounds.GetBounds();
		}

		public override void Draw(RenderTarget render)
		{
			foreach (var surface in Surfaces)
			{
				surface.Draw(render);
			}
		}

		public override void Fill(RenderTarget render, bool isHole)
		{
			//if (IsPositive)
			//{
			//	render.FillGeometry(ShapeGemotry, DefaultBrush);
			//}
			//else
			//{
			//	render.FillGeometry(ShapeGemotry, HoleBrush);
			//}
			foreach (var surface in Surfaces)
			{
				render.FillGeometry(surface.ShapeGemotry, DefaultBrush);
			}
		}
	}
}
