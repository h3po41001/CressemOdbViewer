using System;
using System.Collections.Generic;
using System.Drawing;
using ImageControl.Extension;
using SharpDX.Direct2D1;

namespace ImageControl.Shape.DirectX
{
	internal class DirectPolygon : DirectShape
	{
		private DirectPolygon() : base() { }

		public DirectPolygon(IEnumerable<DirectShape> paths,
			Factory factory, RenderTarget render, Color color) : base(factory, render, color)
		{
			Paths = new List<DirectShape>(paths);
		}

		public IEnumerable<DirectShape> Paths { get; private set; }

		public override void SetShape()
		{
			throw new NotImplementedException();
		}

		public RectangleF GetBounds()
		{
			if (Paths is null)
			{
				return RectangleF.Empty;
			}

			List<RectangleF> bounds = new List<RectangleF>();
			foreach (DirectShape shape in Paths)
			{
				if (shape is DirectPathGeometry path)
				{
					var pathBounds = path.PathGeometry.GetBounds();
					bounds.Add(new RectangleF()
					{
						X = pathBounds.Left,
						Y = pathBounds.Top,
						Width = pathBounds.Right - pathBounds.Left,
						Height = pathBounds.Bottom - pathBounds.Top
					});
				}
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
			foreach (var path in Paths)
			{
				path.Fill(render);
			}
		}
	}
}
