using System.Collections.Generic;
using System.Drawing;
using ImageControl.Shape.DirectX.Interface;
using SharpDX.Direct2D1;

namespace ImageControl.Shape.DirectX
{
	internal class DirectShapeFactory
	{
		private static DirectShapeFactory _instance;

		private DirectShapeFactory()
		{
		}

		public static DirectShapeFactory Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new DirectShapeFactory();
				}
				return _instance;
			}
		}

		public DirectShape CreateDirectShape(IDirectShape shape,
			Factory factory, RenderTarget render, Color color)
		{
			if (shape is null)
			{
				return null;
			}

			if (shape is IDirectArc arc)
			{
				return CreateDirectArc(arc, factory, render, color);
			}
			else if (shape is IDirectEllipse ellipse)
			{
				return CreateDirectEllipse(ellipse, factory, render, color);
			}
			else if (shape is IDirectLine line)
			{
				return CreateDirectLine(line, factory, render, color);
			}
			else if (shape is IDirectPolygon polygon)
			{
				return CreateDirectPolygon(polygon, factory, render, color);
			}
			else if (shape is IDirectRectangle rect)
			{
				return CreateDirectRectangle(rect, factory, render, color);
			}
			else if (shape is IDirectSurface surface)
			{
				return CreateDirectSurface(surface, factory, render, color);
			}

			return null;
		}

		private DirectShape CreateDirectArc(IDirectArc arc,
			Factory factory, RenderTarget render, Color color)
		{
			if (arc is null)
			{
				return null;
			}

			return new DirectArc(arc.Sx, arc.Sy, arc.Ex, arc.Ey,
				arc.Width, arc.Height,
				arc.Rotation, arc.IsLargeArc, arc.IsClockwise,
				factory, render, color);
		}

		private DirectShape CreateDirectLine(IDirectLine line,
			Factory factory, RenderTarget render, Color color)
		{
			if (line is null)
			{
				return null;
			}

			return new DirectLine(line.Sx, line.Sy, line.Ex, line.Ey,
				factory, render, color);
		}

		private DirectShape CreateDirectEllipse(IDirectEllipse ellipse,
			Factory factory, RenderTarget render, Color color)
		{
			if (ellipse is null)
			{
				return null;
			}

			return new DirectEllipse(ellipse.Cx, ellipse.Cy,
				ellipse.Width, ellipse.Height, factory, render, color);
		}

		private DirectShape CreateDirectPolygon(IDirectPolygon polygon,
			Factory factory, RenderTarget render, Color color)
		{
			if (polygon is null)
			{
				return null;
			}

			List<DirectShape> shapes = new List<DirectShape>();
			if (polygon.Shapes != null)
			{
				foreach (var shape in polygon.Shapes)
				{
					if (shape is null)
					{
						continue;
					}

					shapes.Add(CreateDirectShape(shape, factory, render, color));
				}
			}

			return new DirectPolygon(shapes, factory, render, color);
		}

		private DirectShape CreateDirectRectangle(IDirectRectangle rectangle,
			Factory factory, RenderTarget render, Color color)
		{
			throw new System.NotImplementedException();
		}

		private DirectShape CreateDirectSurface(IDirectSurface surface,
			Factory factory, RenderTarget render, Color color)
		{
			if (surface is null)
			{
				return null;
			}

			List<DirectShape> shapes = new List<DirectShape>();
			foreach (var polygon in surface.Polygons)
			{
				shapes.Add(CreateDirectShape(polygon, factory, render, color));
			}

			return new DirectSurface(shapes, factory, render, color);
		}
	}
}
