using System;
using System.Drawing;
using ImageControl.Model.Gdi;

namespace ImageControl.Model
{
	internal abstract class SmartGraphics
	{
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

		public abstract void AddShape(GdiShape gdiShape);

		public abstract void OnDraw();
	}
}
