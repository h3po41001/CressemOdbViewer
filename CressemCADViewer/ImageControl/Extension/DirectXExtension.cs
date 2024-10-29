using System.Drawing;
using ImageControl.Shape.DirectX;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;

namespace ImageControl.Extension
{
	internal static class DirectXExtension
	{
		public static RectangleF ToRectangleF(this RawRectangleF rect)
		{
			return new RectangleF(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
		}

		public static PathGeometry Combine(this Geometry geometry, DirectShape other,
			CombineMode mode, Factory factory)
		{
			if (other is null)
			{
				return null;
			}

			PathGeometry resultGeometry = new PathGeometry(factory);
			if (other is DirectLine line)
			{
				PathGeometry tempGeometry = new PathGeometry(factory);
				using (var sink = tempGeometry.Open())
				{
					sink.BeginFigure(line.StartPt, FigureBegin.Filled);
					sink.AddLine(line.EndPt);
					sink.EndFigure(FigureEnd.Open);
					sink.Close();
				}

				using (var sink = resultGeometry.Open())
				{
					geometry.Combine(tempGeometry, mode, sink);
					sink.Close();
				}
			}
			else if (other is DirectArc arc)
			{
				using (var sink = resultGeometry.Open())
				{
					sink.BeginFigure(arc.StartPt, FigureBegin.Filled);
					sink.AddArc(arc.Arc);
					sink.EndFigure(FigureEnd.Open);
					sink.Close();
				}
			}
			else if (other is DirectEllipse ellipse)
			{
				EllipseGeometry tempGeometry = new EllipseGeometry(
					factory, ellipse.Ellipse);

				using (var sink = resultGeometry.Open())
				{
					geometry.Combine(tempGeometry, mode, sink);
					sink.Close();
				}
			}
			else if (other is DirectRectangle rect)
			{
				RectangleGeometry tempGeometry = new RectangleGeometry(
					factory, rect.Rectangle);

				using (var sink = resultGeometry.Open())
				{
					geometry.Combine(tempGeometry, mode, sink);
					sink.Close();
				}
			}
			else if (other is DirectPolygon polygon)
			{
				using (var sink = resultGeometry.Open())
				{
					sink.Close();
				}

				PathGeometry tempGeometry = new PathGeometry(factory);
				using (var sink = tempGeometry.Open())
				{
					foreach (var shape in polygon.Paths)
					{
						resultGeometry.Combine(shape.ShapeGemotry, mode, sink);
					}

					sink.Close();
				}

				resultGeometry.Dispose();
				resultGeometry = tempGeometry;
			}
			else
			{
				return null;
			}

			return resultGeometry;
		}

		public static Geometry Combine(Geometry src1, Geometry src2, CombineMode mode, Factory factory)
		{
			PathGeometry resultGeometry = new PathGeometry(factory);
			using (var sink = resultGeometry.Open())
			{
				src1.Combine(src2, mode, sink);
				sink.Close();
			}

			return resultGeometry;
		}
	}
}
