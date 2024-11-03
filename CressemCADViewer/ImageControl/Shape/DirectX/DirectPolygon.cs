using System;
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
			Factory factory, RenderTarget render, Color color,
			float skipRatio) : base(isPositive, factory, render, color)
		{
			IsFill = isPositive ? isFill : !isFill;
			Paths = new List<DirectShape>(paths);
			SetShape(skipRatio);
		}

		public bool IsFill { get; private set; }

		public IEnumerable<DirectShape> Paths { get; private set; }

		public override void SetShape(float skipRatio)
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
			SkipSize = new SizeF(
				Math.Abs(Bounds.Width * skipRatio),
				Math.Abs(Bounds.Height * skipRatio));
		}

		public override void Draw(RenderTarget render)
		{
			foreach (var path in Paths)
			{
				path.Draw(render);
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
}
