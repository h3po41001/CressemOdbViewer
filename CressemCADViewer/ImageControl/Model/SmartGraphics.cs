using System;
using System.Drawing;
using ImageControl.Shape.Interface;

namespace ImageControl.Model
{
	internal abstract class SmartGraphics
	{
		public abstract event EventHandler MouseMoveEvent;

		protected SmartGraphics()
		{
		}

		public object GraphicsControl { protected get; set; }

		public PointF MousePos { get; set; } = new PointF();

		public SizeF OffsetSize { get; set; } = new SizeF();

		public float ScreenZoom { get; set; }

		protected float PixelResolution { get; set; }

		protected PointF StartPos { get; set; }

		protected PointF WindowPos { get; set; }

		protected PointF ProductPos { get; set; }

		protected bool MousePressed { get; set; }

		public abstract void Initialize();

		public abstract bool LoadRoi(IShapeBase roiShape);

		public abstract void AddShape(IShapeBase shape);

		public abstract void ClearShape();

		public abstract void OnDraw();
	}
}
