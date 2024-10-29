using System.Drawing;
using ImageControl.Shape.DirectX;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;

namespace ImageControl.Extension
{
	internal static class DirectXObjectExtension
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
				using (var sink = resultGeometry.Open())
				{
					sink.BeginFigure(line.StartPt, FigureBegin.Filled);
					sink.AddLine(line.EndPt);
					sink.EndFigure(FigureEnd.Open);
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
			else if (other is DirectPolygon polygon)
			{
				using (var sink = resultGeometry.Open())
				{
					sink.Close();
				}

				foreach (var shape in polygon.Paths)
				{
					PathGeometry tempGeometry = new PathGeometry(factory);
					using (var sink = tempGeometry.Open())
					{
						resultGeometry.Combine(shape.ShapeGemotry, mode, sink);
						sink.Close();
					}

					resultGeometry.Dispose();
					resultGeometry = tempGeometry;
				}
			}
			else
			{
				return null;
			}

			return resultGeometry;
		}
	}
}
