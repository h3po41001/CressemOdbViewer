using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ImageControl.Extension;
using SharpDX.Direct2D1;

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

				ShapeGemotry = null;

				List<Geometry> geometries = new List<Geometry>();
				bool startedFigure = false;

				PathGeometry pathGeometry = new PathGeometry(Factory);
				using (var sink = pathGeometry.Open())
				{
					foreach (var path in Paths)
					{
						if (path is DirectLine || path is DirectArc)
						{
							if (startedFigure is false)
							{
								if (path is DirectLine firstLine)
								{
									sink.BeginFigure(firstLine.StartPt, FigureBegin.Filled);
									sink.AddLine(firstLine.EndPt);
								}
								else if (path is DirectArc firstArc)
								{
									sink.BeginFigure(firstArc.StartPt, FigureBegin.Filled);
									sink.AddArc(firstArc.Arc);
								}

								startedFigure = true;
							}

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
						else
						{
							geometries.Add(path.ShapeGemotry);
						}
					}

					if (startedFigure is true)
					{
						sink.EndFigure(FigureEnd.Closed);
					}

					sink.Close();
				}

				if (startedFigure is true)
				{
					geometries.Add(pathGeometry);
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
			if (Paths is null)
			{
				return;
			}

			List<RectangleF> bounds = new List<RectangleF>();
			foreach (DirectShape shape in Paths)
			{
				var pathBounds = shape.Bounds;
				bounds.Add(new RectangleF()
				{
					X = pathBounds.Left,
					Y = pathBounds.Top,
					Width = pathBounds.Right - pathBounds.Left,
					Height = pathBounds.Bottom - pathBounds.Top
				});
			}

			Bounds = bounds.GetBounds();
		}

		public override void Draw(RenderTarget render, RectangleF roi)
		{
			if (roi.IntersectsWith(Bounds) is true)
			{
				foreach (var path in Paths)
				{
					path.Draw(render, roi);
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
				if (IsFill)
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
