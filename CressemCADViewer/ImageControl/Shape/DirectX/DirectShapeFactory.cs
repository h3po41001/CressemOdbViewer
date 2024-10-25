using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

		public DirectShape CreateDirectShape(IDirectShape shapeBase,
			Factory factory, RenderTarget render, Color color)
		{
			if (shapeBase is IDirectArc arc)
			{
				return new DirectArc(arc.Sx, arc.Sy, arc.Ex, arc.Ey, 
					arc.Width, arc.Height, 
					arc.Rotation, arc.IsLargeArc, arc.IsClockwise,
					factory, render, color);
			}

			return null;
		}
	}
}
