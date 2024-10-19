using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using ImageControl.Extension;
using ImageControl.Gdi.View;

namespace ImageControl.Model.Gdi
{
	internal class GdiGraphics : SmartGraphics
	{
		public override event EventHandler<Point> CropImageEvent = delegate { };

		private WindowsFormsHost _gdiControl;
		private GdiWinformView _gdiView = new GdiWinformView();
		private Graphics _gdiGraphics;

		private Bitmap _image = null;
		private PointF _imageLT = new PointF(0, 0);

		public GdiGraphics() : base()
		{
			ScreenZoom = 1.0f;
		}

		public override void Initialize()
		{
			_gdiControl = GraphicsControl as WindowsFormsHost;
			_gdiControl.Child = _gdiView;

			_gdiView.GraphicsPaint += GdiPaint;
			_gdiView.GraphicsMouseWheel += GdiMouseWheel;
			_gdiView.GraphicsResize += GdiResize;
			_gdiView.GraphicsMouseDoubleClick += GdiMouseDoubleClick;
			_gdiView.GraphicsMouseDown += GdiMouseDown;
			_gdiView.GraphicsMouseMove += GdiMouseMove;
			_gdiView.GraphicsMouseUp += GdiMouseUp;
			_gdiView.GraphicsPrevKeyDown += GdiPrevkeyDown;
		}

		public override bool LoadImage(Bitmap image)
		{
			if (image is null)
				return false;

			_image = image.Clone() as Bitmap;
			return true;
		}

		public override void OnDraw()
		{
			if (_image is null)
				return;

			_gdiGraphics.ResetTransform();

			_gdiGraphics.InterpolationMode = InterpolationMode.NearestNeighbor;
			_gdiGraphics.PixelOffsetMode = PixelOffsetMode.HighSpeed;

			var center = new PointF(
				_gdiGraphics.ClipBounds.Size.Width / 2, 
				_gdiGraphics.ClipBounds.Size.Height / 2);
			var startXY = new PointF(
				(center.X - _image.Width / 2) / ScreenZoom,
				(center.Y - _image.Height / 2) / ScreenZoom);

			_gdiGraphics.ScaleTransform(ScreenZoom, ScreenZoom);
			_gdiGraphics.TranslateTransform(
				OffsetSize.Width + startXY.X, 
				OffsetSize.Height + startXY.Y);

			_gdiGraphics.DrawImage(_image, 0, 0);
			_gdiGraphics.ResetTransform();
		}

		private void GdiPaint(object sender, Graphics graphics)
		{
			_gdiGraphics = graphics;

			OnDraw();
			_gdiView.Invalidate();
		}

		private void GdiMouseWheel(object sender, MouseEventArgs e)
		{
			float oldZoom = ScreenZoom;
			if (e.Delta > 0)
			{
				ScreenZoom *= 1.3F;
			}
			else
			{
				ScreenZoom *= 0.7F;
			}

			if (ScreenZoom <= 0.05f)
				ScreenZoom = 0.05f;
			else if (ScreenZoom >= 100.0f)
				ScreenZoom = 100.0f;

			MousePos = new PointF(e.X - _gdiView.Location.X, e.Y - _gdiView.Location.Y);

			float oldimagex = (MousePos.X / oldZoom);
			float oldimagey = (MousePos.Y / oldZoom);

			float newimagex = (MousePos.X / ScreenZoom);
			float newimagey = (MousePos.Y / ScreenZoom);

			OffsetSize = new SizeF(OffsetSize.Width + (newimagex - oldimagex),
				OffsetSize.Height + (newimagey - oldimagey));

			_gdiView.Invalidate();
		}

		private void GdiResize(object sender, EventArgs e)
		{
			_gdiView.Invalidate();
		}

		private void GdiMouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (e.Button is MouseButtons.Left)
			{
				MousePos = new PointF(e.X - _gdiView.Location.X, e.Y - _gdiView.Location.Y);
				
				var displayCenter = _gdiView.Bounds.GetCenterF();
				float newimagex = ((displayCenter.X - MousePos.X) / ScreenZoom);
				float newimagey = ((displayCenter.Y - MousePos.Y) / ScreenZoom);

				OffsetSize = new SizeF(OffsetSize.Width + newimagex,
					OffsetSize.Height + newimagey);

				_gdiView.Invalidate();
			}
		}

		private void GdiMouseDown(object sender, MouseEventArgs e)
		{
			MousePressed = true;

			StartPos = new PointF(OffsetSize.Width, OffsetSize.Height);
			MouseDown = new PointF(e.X, e.Y);

			_gdiView.Invalidate();
		}

		private void GdiMouseMove(object sender, MouseEventArgs e)
		{
			if (MousePressed && e.Button is MouseButtons.Left)
			{
				float deltaX = e.Location.X - MouseDown.X;
				float deltaY = e.Location.Y - MouseDown.Y;

				OffsetSize = new SizeF(StartPos.X + (deltaX / ScreenZoom),
					StartPos.Y + (deltaY / ScreenZoom));

				_gdiView.Invalidate();
			}
		}

		private void GdiMouseUp(object sender, MouseEventArgs e)
		{
			if (MousePressed && e.Button is MouseButtons.Left)
			{
				Point point = new Point(
					(int)(e.X / ScreenZoom - OffsetSize.Width),
					(int)(e.Y / ScreenZoom - OffsetSize.Height));

				if (_image is null || _image.Size.IsEmpty is true)
					return;

				if (point.X <= 0 || point.X >= _image.Width ||
					point.Y <= 0 || point.Y >= _image.Height)
				{
					return;
				}
			}

			MousePressed = false;
			_gdiView.Invalidate();
		}

		private void GdiPrevkeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
			{
				var displayCenter = _gdiView.Bounds.GetCenter();
				var realOffset = new Size()
				{
					Width = (int)(-OffsetSize.Width),
					Height = (int)(-OffsetSize.Height),
				};

				CropImageEvent(this, new Point()
				{
					X = realOffset.Width + (int)(displayCenter.X / ScreenZoom),
					Y = realOffset.Height + (int)(displayCenter.Y / ScreenZoom),
				});
			}
		}
	}
}
