using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using ImageControl.Extension;
using ImageControl.Gdi.View;
using ImageControl.Model.Shape.Gdi;
using ImageControl.Shape.Gdi;
using ImageControl.Shape.Gdi.Interface;
using ImageControl.Shape.Interface;

namespace ImageControl.Model.Gdi
{
	internal class GdiGraphics : SmartGraphics
	{
		public override event EventHandler MouseMoveEvent = delegate { };

		private readonly GdiWinformView _gdiView = new GdiWinformView();
		private readonly List<GdiShape> _gdiProfileShapes = new List<GdiShape>();
		private readonly List<GdiShape> _gdiShapes = new List<GdiShape>();
		private WindowsFormsHost _gdiControl;
		private Graphics _gdiGraphics;
		private Bitmap _image = null;
		private SolidBrush _background = new SolidBrush(Color.Black);

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

		public override bool LoadProfiles(IEnumerable<IGraphicsList> list)
		{
			if (list is null)
			{
				return false;
			}

			List<RectangleF> rois = new List<RectangleF>();
			foreach (var profile in list)
			{
				foreach (var shape in profile.Shapes)
				{
					if (shape is null)
					{
						continue;
					}

					GdiSurface surface = GdiShapeFactory.Instance.CreateGdiShape(profile.IsPositive, (dynamic)shape);
					if (surface is null)
					{
						continue;
					}

					_gdiProfileShapes.Add(surface);
					rois.Add(surface.GetBounds());
				}
			}

			if (rois.Any() is true)
			{
				Roi = rois.GetBounds();
				_image = new Bitmap((int)(Roi.Width + 0.5f), (int)(Roi.Height + 0.5f));

				// 화면에 맞추기 위함
				ScreenZoom = (float)_gdiControl.RenderSize.Width / Roi.Width;
				if ((float)_gdiControl.RenderSize.Height / Roi.Height < ScreenZoom)
				{
					ScreenZoom = (float)_gdiControl.RenderSize.Height / Roi.Height;
				}

				WindowPos = new PointF(
					(float)_gdiControl.RenderSize.Width / 2,
					(float)_gdiControl.RenderSize.Height / 2);

				ProductPos = new PointF(Roi.Width / 2, Roi.Height / 2);

				OffsetSize = new SizeF(
					WindowPos.X - ProductPos.X * ScreenZoom,
					WindowPos.Y - ProductPos.Y * ScreenZoom);

				return true;
			}

			return false;
		}

		public override void AddShapes(IGraphicsList list)
		{
			if (list is null)
			{
				return;
			}

			foreach (var shape in list.Shapes)
			{
				if (shape is null)
				{
					continue;
				}

				_gdiShapes.Add(GdiShapeFactory.Instance.CreateGdiShape(list.IsPositive,
					(dynamic)shape));
			}
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
			_gdiGraphics.SetClip(new Rectangle(0, 0, _gdiView.Width, _gdiView.Height));
			_gdiGraphics.TranslateTransform(
				OffsetSize.Width - Roi.X * ScreenZoom,
				OffsetSize.Height - Roi.Y * ScreenZoom);
			_gdiGraphics.ScaleTransform(ScreenZoom, ScreenZoom);

			_gdiGraphics.DrawImage(_image, Roi);
			_gdiGraphics.FillRectangle(_background, Roi.X, Roi.Y, Roi.Width, Roi.Height);

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
			foreach (var shape in _gdiProfileShapes)
			{
				if (shape is null)
				{
					continue;
				}

				shape.Draw(_gdiGraphics);
			}

			foreach (var graphics in _gdiShapes)
			{
				if (graphics is null)
				{
					continue;
				}

				graphics.Fill(_gdiGraphics);
			}
		}

		private void GdiMouseWheel(object sender, MouseEventArgs e)
		{
			if (MousePressed is true)
			{
				return;
			}

			if (e.Delta > 0)
			{
				ScreenZoom *= 1.1F;
			}
			else
			{
				ScreenZoom *= 0.9F;
			}

			if (ScreenZoom <= 0.05f)
			{
				ScreenZoom = 0.05f;
			}
			else if (ScreenZoom >= 100.0f)
			{
				ScreenZoom = 100.0f;
			}

			float offsetX = WindowPos.X - ProductPos.X * ScreenZoom;
			float offsetY = WindowPos.Y - ProductPos.Y * ScreenZoom;
			OffsetSize = new SizeF(offsetX, offsetY);

			WindowPos = new PointF(e.X, e.Y);

			float productX = (WindowPos.X - OffsetSize.Width) / ScreenZoom;
			float productY = (WindowPos.Y - OffsetSize.Height) / ScreenZoom;
			ProductPos = new PointF(productX, productY);

			MouseMoveEvent(this, null);

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
				//MousePos = new PointF(
				//	((e.X - _startPos.X) - OffsetSize.Width) / ScreenZoom,
				//	((e.Y - _startPos.Y) - OffsetSize.Height) / ScreenZoom);

				//var displayCenter = _gdiView.Bounds.GetCenterF();
				//float newimagex = ((displayCenter.X - MousePos.X) / ScreenZoom);
				//float newimagey = ((displayCenter.Y - MousePos.Y) / ScreenZoom);

				//OffsetSize = new SizeF(OffsetSize.Width + newimagex,
				//	OffsetSize.Height + newimagey);

				_gdiView.Invalidate();
			}
		}

		private void GdiMouseDown(object sender, MouseEventArgs e)
		{
			MousePressed = true;

			StartPos = new PointF(OffsetSize.Width, OffsetSize.Height);
			WindowPos = new PointF(e.X, e.Y);

			_gdiView.Invalidate();
		}

		private void GdiMouseMove(object sender, MouseEventArgs e)
		{
			MousePos = new PointF(e.X - OffsetSize.Width, e.Y - OffsetSize.Height);

			if (MousePressed && e.Button is MouseButtons.Left)
			{
				float deltaX = e.X - WindowPos.X;
				float deltaY = e.Y - WindowPos.Y;

				OffsetSize = new SizeF(StartPos.X + deltaX,
					StartPos.Y + deltaY);

				_gdiView.Invalidate();
			}

			MouseMoveEvent(this, null);
		}

		private void GdiMouseUp(object sender, MouseEventArgs e)
		{
			if (MousePressed && e.Button is MouseButtons.Left)
			{
				WindowPos = new PointF(e.X, e.Y);

				float productX = (WindowPos.X - OffsetSize.Width) / ScreenZoom;
				float productY = (WindowPos.Y - OffsetSize.Height) / ScreenZoom;
				ProductPos = new PointF(productX, productY);

				_gdiView.Invalidate();
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
				// 화면에 맞추기 위함
				ScreenZoom = (float)_gdiControl.RenderSize.Width / Roi.Width;
				if ((float)_gdiControl.RenderSize.Height / Roi.Height < ScreenZoom)
				{
					ScreenZoom = (float)_gdiControl.RenderSize.Height / Roi.Height;
				}

				WindowPos = new PointF(
					(float)_gdiControl.RenderSize.Width / 2,
					(float)_gdiControl.RenderSize.Height / 2);

				ProductPos = new PointF(Roi.Width / 2, Roi.Height / 2);

				OffsetSize = new SizeF(
					WindowPos.X - ProductPos.X * ScreenZoom,
					WindowPos.Y - ProductPos.Y * ScreenZoom);
			}
		}
	}
}
