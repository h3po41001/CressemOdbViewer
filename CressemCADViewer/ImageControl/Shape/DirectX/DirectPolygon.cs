using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
			IsFill = isPositive ? isFill : !isFill;
			Paths = new List<DirectShape>(paths);
			SetShape();
		}

		public bool IsFill { get; private set; }

		public IEnumerable<DirectShape> Paths { get; private set; }

		public override void SetShape()
		{
			try
			{
				if (Paths is null || Paths.Count() == 0)
				{
					return;
				}

				DirectLine firstLine = (DirectLine)Paths.FirstOrDefault(x => x is DirectLine);

				PathGeometry pathGeometry = new PathGeometry(Factory);
				using (var sink = pathGeometry.Open())
				{
					sink.BeginFigure(firstLine.EndPt, FigureBegin.Filled);

					foreach (var path in Paths.Skip(1))
					{
						if (path is null)
						{
							continue;
						}

						if (path is DirectLine line)
						{
							sink.AddLine(line.EndPt);
						}
						else if (path is DirectArc arc)
						{
							sink.AddArc(arc.Arc);
						}
					}

					sink.EndFigure(FigureEnd.Open);
					sink.Close();
				}

				List<Geometry> geometries = new List<Geometry>
				{
					pathGeometry
				};

				geometries.AddRange(Paths.SkipWhile(x => x is DirectArc || x is DirectLine).Select(x => x.ShapeGemotry));
				ShapeGemotry = new GeometryGroup(Factory, FillMode.Winding, geometries.ToArray());
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
