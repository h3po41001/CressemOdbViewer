using System;
using System.Drawing;

namespace ImageControl.Model
{
	internal abstract class SmartGraphics
	{
		public virtual event EventHandler<Point> CropImageEvent = delegate { };

		protected SmartGraphics()
		{
		}

		public object GraphicsControl { protected get; set; }

		protected PointF MousePos { get; set; } = new PointF();

		protected SizeF OffsetSize { get; set; } = new SizeF();

		protected float ScreenZoom { get; set; }

		protected PointF StartPos { get; set; }

		protected PointF MouseDown { get; set; }

		protected bool MousePressed { get; set; }

		public abstract void Initialize();

		public abstract bool LoadImage(Bitmap image);

		public abstract void OnDraw();
	}
}
