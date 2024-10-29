using System.Collections.Generic;
using System.Drawing;
using ImageControl.Extension;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;

namespace ImageControl.Shape.DirectX
{
	internal class DirectPolygon : DirectShape
	{
		private DirectPolygon() : base() { }

		public DirectPolygon(bool isPositive,
			bool isFill, IEnumerable<DirectShape> paths,
			Factory factory, RenderTarget render, Color color) : base(isPositive, factory, render, color)
		{
			IsFill = isFill;
			Paths = new List<DirectShape>(paths);
			SetShape();
		}

		public bool IsFill { get; private set; }

		public IEnumerable<DirectShape> Paths { get; private set; }

		public override void SetShape()
		{
			try
			{
				ShapeGemotry = new PathGeometry(Factory);
				using (var temp = ((PathGeometry)ShapeGemotry).Open())
				{
					temp.Close();
				}

				foreach (var shape in Paths)
				{
					PathGeometry resultGeometry = null;
					if (IsFill is true)
					{
						resultGeometry = ShapeGemotry.Combine(shape, CombineMode.Union, Factory);
					}
					else
					{
						resultGeometry = ShapeGemotry.Combine(shape, CombineMode.Xor, Factory);
					}

					ShapeGemotry.Dispose();
					ShapeGemotry = resultGeometry;
				}
			}
			catch (System.Exception)
			{
				ShapeGemotry.Dispose();
				throw;
			}
		}

		public override RectangleF GetBounds()
		{
			if (Paths is null)
			{
				return RectangleF.Empty;
			}

			List<RectangleF> bounds = new List<RectangleF>();
			foreach (DirectShape shape in Paths)
			{
				var pathBounds = shape.GetBounds();
				bounds.Add(new RectangleF()
				{
					X = pathBounds.Left,
					Y = pathBounds.Top,
					Width = pathBounds.Right - pathBounds.Left,
					Height = pathBounds.Bottom - pathBounds.Top
				});
			}

			return bounds.GetBounds();
		}

		public override void Draw(RenderTarget render)
		{
			foreach (var path in Paths)
			{
				path.Draw(render);
			}
		}

		public override void Fill(RenderTarget render)
		{
			if (IsFill is true)
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
