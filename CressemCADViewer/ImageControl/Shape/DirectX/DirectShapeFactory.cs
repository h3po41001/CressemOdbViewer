﻿using System.Collections.Generic;
using System.Drawing;
using ImageControl.Shape.DirectX.Interface;
using ImageControl.Shape.Interface;
using SharpDX.Direct2D1;

namespace ImageControl.Shape.DirectX
{
	internal class DirectShapeFactory
	{
		private static DirectShapeFactory _instance;

		private DirectShapeFactory() { }

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

		public DirectShape CreateDirectShape(bool isPositive, IGraphicsShape shape,
			Factory factory, RenderTarget render, Color color, float skipRatio)
		{
			if (shape is null)
			{
				return null;
			}

			if (shape is IDirectArc arc)
			{
				return CreateDirectArc(isPositive, arc, factory, render, color, skipRatio);
			}
			else if (shape is IDirectEllipse ellipse)
			{
				return CreateDirectEllipse(isPositive, ellipse, factory, render, color, skipRatio);
			}
			else if (shape is IDirectLine line)
			{
				return CreateDirectLine(isPositive, line, factory, render, color, skipRatio);
			}
			else if (shape is IDirectPolygon polygon)
			{
				return CreateDirectPolygon(isPositive, polygon, factory, render, color, skipRatio);
			}
			else if (shape is IDirectRectangle rect)
			{
				return CreateDirectRectangle(isPositive, rect, factory, render, color, skipRatio);
			}
			else if (shape is IDirectSurface surface)
			{
				return CreateDirectSurface(isPositive, surface, factory, render, color, skipRatio);
			}

			return null;
		}

		private DirectShape CreateDirectArc(bool isPositive, IDirectArc arc,
			Factory factory, RenderTarget render, Color color, float skipRatio)
		{
			if (arc is null)
			{
				return null;
			}

			return new DirectArc(isPositive, arc.Sx, arc.Sy, arc.Ex, arc.Ey,
				arc.RadiusWidth, arc.RadiusHeight,
				arc.Rotation, arc.IsLargeArc, arc.IsClockwise, arc.LineWidth,
				factory, render, color, skipRatio);
		}

		private DirectShape CreateDirectLine(bool isPositive, IDirectLine line,
			Factory factory, RenderTarget render, Color color, float skipRatio)
		{
			if (line is null)
			{
				return null;
			}

			return new DirectLine(isPositive,
				line.Sx, line.Sy,
				line.Ex, line.Ey, line.LineWidth,
				factory, render, color, skipRatio);
		}

		private DirectShape CreateDirectEllipse(bool isPositive, IDirectEllipse ellipse,
			Factory factory, RenderTarget render, Color color, float skipRatio)
		{
			if (ellipse is null)
			{
				return null;
			}

			return new DirectEllipse(isPositive, ellipse.Cx, ellipse.Cy,
				ellipse.RadiusX, ellipse.RadiusY, factory, render, color, skipRatio);
		}

		private DirectShape CreateDirectPolygon(bool isPositive, IDirectPolygon polygon,
			Factory factory, RenderTarget render, Color color, float skipRatio)
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

					shapes.Add(CreateDirectShape(isPositive, shape, factory, render, color, skipRatio));
				}
			}

			return new DirectPolygon(isPositive,
				polygon.IsFill, shapes, factory, render, color, skipRatio);
		}

		private DirectShape CreateDirectRectangle(bool isPositive, IDirectRectangle rectangle,
			Factory factory, RenderTarget render, Color color, float skipRatio)
		{
			if (rectangle is null)
			{
				return null;
			}

			return new DirectRectangle(isPositive, rectangle.Left, rectangle.Top,
				rectangle.Right, rectangle.Bottom, factory, render, color, skipRatio);
		}

		private DirectShape CreateDirectSurface(bool isPositive, IDirectSurface surface,
			Factory factory, RenderTarget render, Color color, float skipRatio)
		{
			if (surface is null)
			{
				return null;
			}

			List<DirectShape> shapes = new List<DirectShape>();
			foreach (var polygon in surface.Polygons)
			{
				shapes.Add(CreateDirectShape(isPositive, polygon, factory, render, color, skipRatio));
			}

			return new DirectSurface(isPositive, shapes, factory, render, color, skipRatio);
		}
	}
}
