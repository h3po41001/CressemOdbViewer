using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using ImageControl.Extension;
using ImageControl.Gdi.View;
using ImageControl.Model.Gdi.Shape;

namespace ImageControl.Model.Gdi
{
	internal class GdiGraphics : SmartGraphics
	{
		public override event EventHandler MouseMoveEvent = delegate { };

		private readonly GdiWinformView _gdiView = new GdiWinformView();
		private readonly List<GdiShape> _gdiShapes = new List<GdiShape>();
		private WindowsFormsHost _gdiControl;
		private Graphics _gdiGraphics;
		private Bitmap _image = null;
		private RectangleF _roi = new RectangleF();

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

		public override bool LoadRoi(RectangleF roi, float pixelResolution)
		{
			if (roi.IsEmpty is true)
			{
				return false;
			}

			PixelResolution = pixelResolution;
			_roi = new RectangleF(
				roi.X * pixelResolution, roi.Y * pixelResolution,
				roi.Width * pixelResolution, roi.Height * pixelResolution);

			_image = new Bitmap(
				(int)(roi.Width * pixelResolution), 
				(int)(roi.Height * pixelResolution));

			return true;
		}

		public override void AddShape(GdiShape gdiShape)
		{
			_gdiShapes.Add(gdiShape);
		}

		public override void ClearShape()
		{
			_gdiShapes.Clear();
		}

		public override void OnDraw()
		{
			if (_image is null)
			{
				return;
			}

			_gdiGraphics.ResetTransform();

			_gdiGraphics.InterpolationMode = InterpolationMode.NearestNeighbor;
			_gdiGraphics.PixelOffsetMode = PixelOffsetMode.HighSpeed;

			_gdiGraphics.ScaleTransform(ScreenZoom, ScreenZoom);
			_gdiGraphics.TranslateTransform(
				OffsetSize.Width + _roi.X / ScreenZoom,
				OffsetSize.Height + _roi.Y / ScreenZoom);

			_gdiGraphics.DrawImage(_image, _roi);
			_gdiGraphics.DrawRectangle(new Pen(Color.Red, 0.1f), _roi.X, _roi.Y, _roi.Width, _roi.Height);
			DrawShapes();
			_gdiGraphics.ResetTransform();
		}

		private void GdiPaint(object sender, Graphics graphics)
		{
			_gdiGraphics = graphics;

			OnDraw();
			_gdiView.Invalidate();
		}

		private void DrawShapes()
		{
			foreach (var shape in _gdiShapes)
			{
				shape.Draw(_gdiGraphics, new Pen(Color.Red, 0.1f));
			}
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
			MousePos = new PointF(
				e.X / ScreenZoom - OffsetSize.Width,
				e.Y / ScreenZoom - OffsetSize.Height);

			if (MousePressed && e.Button is MouseButtons.Left)
			{
				float deltaX = e.Location.X - MouseDown.X;
				float deltaY = e.Location.Y - MouseDown.Y;

				OffsetSize = new SizeF(StartPos.X + (deltaX / ScreenZoom),
					StartPos.Y + (deltaY / ScreenZoom));

				_gdiView.Invalidate();
			}

			MouseMoveEvent(this, null);
		}

		private void GdiMouseUp(object sender, MouseEventArgs e)
		{
			if (MousePressed && e.Button is MouseButtons.Left)
			{
				Point point = new Point(
					(int)(e.X / ScreenZoom - OffsetSize.Width),
					(int)(e.Y / ScreenZoom - OffsetSize.Height));

				if (_image is null || _image.Size.IsEmpty is true)
				{ 
					return; 
				}

				if (point.X > 0 && point.X < _image.Width &&
					point.Y > 0 && point.Y < _image.Height)
				{
					_gdiView.Invalidate();
					return;
				}
			}

			MousePressed = false;
		}

		private void GdiPrevkeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
			{
			}
			else if (e.KeyCode is Keys.Home)
			{
				ScreenZoom = 1.0f;
				OffsetSize = new SizeF();
			}
		}
	}
}
