using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using ImageControl.Extension;
using ImageControl.Gdi.View;
using ImageControl.Shape.DirectX;
using ImageControl.Shape.Interface;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;
using Device = SharpDX.Direct3D11.Device;

namespace ImageControl.Model.DirectX
{
	internal class DirectXGraphics : SmartGraphics
	{
		public override event EventHandler MouseMoveEvent = delegate { };
		private readonly float DEFAULT_DPI = 96.0f;
		private readonly float SKIP_RATIO = 1000000000;

		private readonly List<DirectShape> _directProfileShapes = new List<DirectShape>();
		private readonly List<DirectShape> _directShapes = new List<DirectShape>();
		private DirectXWinformView _directXView = new DirectXWinformView();
		private WindowsFormsHost _directXControl;

		// 3d
		private Device _d3dDevice;
		private SwapChain1 _swapChain;

		// main
		private DeviceContext _deviceContext;
		private Bitmap1 _mainBitmap;

		// shape
		private SharpDX.Direct3D11.Texture2D _shapeTexture;
		private Bitmap1 _shapeBitmap;

		// 2d
		private SharpDX.DXGI.Device _dxgiDevice;
		private SharpDX.Direct2D1.Device1 _d2dDevice;

		private SharpDX.Direct2D1.Factory2 _d2dFactory;
		private Timer _renderTimer;

		private bool _zoomMousePressed = false;
		private bool _zoomIsReady = false;
		private PointF _zoomStartPos = new PointF();
		private float _zoomLineWidth = 1f;
		private RectangleF _zoomedRoi = new RectangleF();
		private SolidColorBrush _zoomBrush;

		private bool _isUpdate = false;
		private RawMatrix3x2 _transformMatrix = new RawMatrix3x2();
		private RectangleF _currentRoi = new RectangleF();
		private PointF _mousePos = new PointF();

		public override void Initialize()
		{
			ResetView();

			_directXControl = GraphicsControl as WindowsFormsHost;
			_directXControl.Child = _directXView;

			RenderStart();

			_directXView.GraphicsResize += OnResize;
			_directXView.GraphicsMouseDown += OnMouseDown;
			_directXView.GraphicsMouseMove += OnMouseMove;
			_directXView.GraphicsMouseUp += OnMouseUp;
			_directXView.GraphicsMouseWheel += OnMouseWheel;
			_directXView.GraphicsPrevKeyDown += OnPrevkeyDown;
		}

		public override bool LoadProfiles(IEnumerable<IGraphicsList> profileShapes)
		{
			if (profileShapes is null)
			{
				return false;
			}

			List<RectangleF> rois = new List<RectangleF>();
			foreach (var profileShape in profileShapes)
			{
				if (profileShape is null)
				{
					continue;
				}

				foreach (var shape in profileShape.Shapes)
				{
					DirectSurface surface = DirectShapeFactory.Instance.CreateDirectShape(
						profileShape.IsPositive, (dynamic)shape,
						_d2dFactory, _deviceContext, Color.White, SKIP_RATIO);

					if (surface is null)
					{
						continue;
					}

					_directProfileShapes.Add(surface);
					rois.Add(surface.Bounds);
				}
			}

			if (rois.Any() is true)
			{
				Roi = rois.GetBounds();

				OnResize(null, null);

				AlignToScreenCenter();
				UpdateMatrix(true, true);

				return true;
			}

			return false;
		}

		public override void AddShapes(IGraphicsList shapes)
		{
			if (shapes is null)
			{
				return;
			}

			foreach (var shape in shapes.Shapes)
			{
				if (shape is null)
				{
					continue;
				}

				_directShapes.Add(DirectShapeFactory.Instance.CreateDirectShape(
					shapes.IsPositive, (dynamic)shape,
					_d2dFactory, _deviceContext, Color.DarkGreen, SKIP_RATIO));
			}
		}

		public override void ClearShape()
		{
			if (_directShapes is null || _directProfileShapes is null ||
				_deviceContext is null || _swapChain is null)
			{
				return;
			}

			_directShapes.Clear();
			_directProfileShapes.Clear();
		}

		public override void OnDraw()
		{
			if (_deviceContext is null)
			{
				return;
			}

			if (_isUpdate is true)
			{
				ShapeRenderStart();
				_isUpdate = false;
			}

			_mainBitmap.CopyFromBitmap(_shapeBitmap);
			_deviceContext.BeginDraw();

			if (_zoomMousePressed is true)
			{
				_deviceContext.DrawRectangle(new RawRectangleF(
					_zoomStartPos.X, _zoomStartPos.Y, ProductPos.X, ProductPos.Y),
					_zoomBrush, _zoomLineWidth);
			}

			_deviceContext.EndDraw();
			_swapChain.Present(1, PresentFlags.None);
		}

		private void OnResize(object sender, EventArgs e)
		{
			if (_swapChain is null)
			{
				return;
			}

			Utilities.Dispose(ref _zoomBrush);
			Utilities.Dispose(ref _shapeTexture);
			Utilities.Dispose(ref _shapeBitmap);
			Utilities.Dispose(ref _mainBitmap);
			Utilities.Dispose(ref _deviceContext);

			_swapChain.ResizeBuffers(1, _directXView.ClientSize.Width, _directXView.ClientSize.Height,
				Format.R8G8B8A8_UNorm, SwapChainFlags.None);

			InitRender();

			_zoomBrush = new SolidColorBrush(_deviceContext, new RawColor4(1, 1, 1, 1));
			_directXView.Invalidate();
		}

		private void OnMouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button is MouseButtons.Right)
			{
				MousePressed = true;
			}
			else
			if (e.Button is MouseButtons.Left)
			{
				if (_zoomIsReady is true)
				{
					RectangleF roi = new RectangleF()
					{
						X = _zoomStartPos.X,
						Y = _zoomStartPos.Y,
						Width = ProductPos.X - _zoomStartPos.X,
						Height = ProductPos.Y - _zoomStartPos.Y,
					};

					if (roi.Width >= 1 && roi.Height >= 1)
					{
						ScreenZoom = _directXView.ClientSize.Width / roi.Width;
						if (_directXView.ClientSize.Height / roi.Height < ScreenZoom)
						{
							ScreenZoom = _directXView.ClientSize.Height / roi.Height;
						}

						float offsetX = _directXView.ClientSize.Width / 2 - roi.GetCenterF().X * ScreenZoom;
						float offsetY = _directXView.ClientSize.Height / 2 - roi.GetCenterF().Y * ScreenZoom;
						OffsetSize = new SizeF(offsetX, offsetY);

						_zoomedRoi = new RectangleF(
							ProductPos.X - _directXView.ClientSize.Width / 2 / ScreenZoom,
							ProductPos.Y - _directXView.ClientSize.Height / 2 / ScreenZoom,
							_directXView.ClientSize.Width / ScreenZoom,
							_directXView.ClientSize.Height / ScreenZoom);

						UpdateMatrix(true, true);
					}

					_zoomIsReady = false;
					_zoomMousePressed = false;
				}
				else
				{
					_zoomMousePressed = true;

					float productX = (e.X - OffsetSize.Width) / ScreenZoom;
					float productY = (e.Y - OffsetSize.Height) / ScreenZoom;
					_zoomStartPos = new PointF(productX, productY);

					_zoomLineWidth = _zoomedRoi.Width / 1000;
				}
			}
			else
			{
				return;
			}

			StartPos = new PointF(OffsetSize.Width, OffsetSize.Height);
			WindowPos = new PointF(e.X, e.Y);
		}

		private void OnMouseMove(object sender, MouseEventArgs e)
		{
			_mousePos = new PointF(e.X, e.Y);

			float productX = (e.X - OffsetSize.Width) / ScreenZoom;
			float productY = (e.Y - OffsetSize.Height) / ScreenZoom;

			ProductPos = new PointF(productX, productY);
			MousePos = new PointF(productX, productY);

			if (MousePressed && e.Button is MouseButtons.Right)
			{
				float deltaX = e.X - WindowPos.X;
				float deltaY = e.Y - WindowPos.Y;

				OffsetSize = new SizeF(StartPos.X + deltaX, StartPos.Y + deltaY);
				UpdateMatrix(false, true);
			}

			MouseMoveEvent(this, null);
		}

		private void OnMouseUp(object sender, MouseEventArgs e)
		{
			if (MousePressed && e.Button is MouseButtons.Right)
			{
				WindowPos = new PointF(e.X, e.Y);

				float productX = (WindowPos.X - OffsetSize.Width) / ScreenZoom;
				float productY = (WindowPos.Y - OffsetSize.Height) / ScreenZoom;
				ProductPos = new PointF(productX, productY);

				UpdateMatrix(false, true);
			}
			else if (_zoomMousePressed && e.Button is MouseButtons.Left)
			{
				_zoomIsReady = true;
			}

			MousePressed = false;
		}

		private void OnMouseWheel(object sender, MouseEventArgs e)
		{
			ZoomInOut(e.Delta <= 0);
		}

		private void OnPrevkeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			if (e.KeyCode is Keys.Home)
			{
				AlignToScreenCenter();
				UpdateMatrix(true, true);
			}
			else if (e.KeyCode is Keys.PageUp)
			{
				ZoomInOut(false);
			}
			else if (e.KeyCode is Keys.PageDown)
			{
				ZoomInOut(true);
			}
		}

		private void ResetView()
		{
			if (_renderTimer != null)
			{
				_renderTimer.Stop();
				_renderTimer.Dispose();
			}

			_directXView.GraphicsResize -= OnResize;
			_directXView.GraphicsMouseDown -= OnMouseDown;
			_directXView.GraphicsMouseMove -= OnMouseMove;
			_directXView.GraphicsMouseUp -= OnMouseUp;
			_directXView.GraphicsPrevKeyDown -= OnPrevkeyDown;

			Utilities.Dispose(ref _zoomBrush);
			Utilities.Dispose(ref _shapeTexture);
			Utilities.Dispose(ref _shapeBitmap);
			Utilities.Dispose(ref _mainBitmap);
			Utilities.Dispose(ref _deviceContext);

			_directXView.Dispose();
			_directXView = new DirectXWinformView();
		}

		private void DrawShapes()
		{
			foreach (var shape in _directProfileShapes)
			{
				if (shape is null)
				{
					continue;
				}

				shape.Draw(_deviceContext);
			}

			foreach (var shape in _directShapes)
			{
				if (shape is null)
				{
					continue;
				}

				shape.Fill(_deviceContext, false, _currentRoi);
			}
		}

		private void RenderStart()
		{
			SwapChainDescription swapChainDesc = new SwapChainDescription()
			{
				BufferCount = 1,
				ModeDescription = new ModeDescription(
					_directXView.ClientSize.Width,
					_directXView.ClientSize.Height,
					new Rational(60, 1), Format.R8G8B8A8_UNorm),
				IsWindowed = true,
				OutputHandle = _directXView.Handle,
				SampleDescription = new SampleDescription(1, 0),
				SwapEffect = SwapEffect.Sequential,
				Usage = Usage.RenderTargetOutput,
			};

			// Direct3D11 장치 및 SwapChain 생성
			Device.CreateWithSwapChain(SharpDX.Direct3D.DriverType.Hardware,
				SharpDX.Direct3D11.DeviceCreationFlags.BgraSupport,
				swapChainDesc,
				out _d3dDevice,
				out SwapChain swap);

			_swapChain = swap.QueryInterface<SwapChain1>();

			// Direct2D Factory 생성
			_d2dFactory = new SharpDX.Direct2D1.Factory2(FactoryType.SingleThreaded, DebugLevel.None);
			_dxgiDevice = _d3dDevice.QueryInterface<SharpDX.DXGI.Device>();
			_d2dDevice = new SharpDX.Direct2D1.Device1(_d2dFactory, _dxgiDevice);

			InitRender();

			_renderTimer = new Timer { Interval = 16 }; // 약 10FPS
			_renderTimer.Tick += RenderTimer_Tick;
			_renderTimer.Start();
		}

		private void InitRender()
		{
			_deviceContext = new DeviceContext(_d2dDevice, DeviceContextOptions.EnableMultithreadedOptimizations);

			using (var backBuffer = _swapChain.GetBackBuffer<SharpDX.Direct3D11.Texture2D>(0))
			{
				using (var surface = backBuffer.QueryInterface<Surface>())
				{
					BitmapProperties1 prop = new BitmapProperties1()
					{
						PixelFormat = new PixelFormat(Format.R8G8B8A8_UNorm, SharpDX.Direct2D1.AlphaMode.Premultiplied),
						DpiX = DEFAULT_DPI,
						DpiY = DEFAULT_DPI,
						BitmapOptions = BitmapOptions.CannotDraw | BitmapOptions.Target,
					};

					_mainBitmap = new Bitmap1(_deviceContext, surface, prop);
					_deviceContext.Target = _mainBitmap;
				}
			}

			var overlayRenderTargetDesc = new SharpDX.Direct3D11.Texture2DDescription()
			{
				Format = Format.R8G8B8A8_UNorm,
				ArraySize = 1,
				MipLevels = 1,
				Width = _directXView.ClientSize.Width,
				Height = _directXView.ClientSize.Height,
				SampleDescription = new SampleDescription(1, 0),
				Usage = SharpDX.Direct3D11.ResourceUsage.Default,
				BindFlags = SharpDX.Direct3D11.BindFlags.RenderTarget,
				CpuAccessFlags = SharpDX.Direct3D11.CpuAccessFlags.None,
				OptionFlags = SharpDX.Direct3D11.ResourceOptionFlags.None,
			};

			_shapeTexture = new SharpDX.Direct3D11.Texture2D(_d3dDevice, overlayRenderTargetDesc);
			InitTexture(_shapeTexture, out _shapeBitmap);

			_directXView.Invalidate();
			_zoomBrush = new SolidColorBrush(_deviceContext, new RawColor4(1, 1, 1, 1));
		}

		private void InitTexture(SharpDX.Direct3D11.Texture2D texture, out Bitmap1 bitmap)
		{
			bitmap = null;

			using (var surface = texture.QueryInterface<Surface>())
			{
				if (surface != null)
				{
					BitmapProperties1 prop = new BitmapProperties1()
					{
						PixelFormat = new PixelFormat(Format.R8G8B8A8_UNorm, SharpDX.Direct2D1.AlphaMode.Premultiplied),
						DpiX = DEFAULT_DPI,
						DpiY = DEFAULT_DPI,
						BitmapOptions = BitmapOptions.CannotDraw | BitmapOptions.Target,
					};

					bitmap = new Bitmap1(_deviceContext, surface, prop);
				}
			}
		}

		private void ShapeRenderStart()
		{
			_deviceContext.Target = _shapeBitmap;
			{
				_deviceContext.BeginDraw();
				_deviceContext.Clear(new RawColor4(0, 0, 0, 1));

				DrawShapes();

				_deviceContext.EndDraw();
			}
			_deviceContext.Target = _mainBitmap;
		}

		private void UpdateMatrix(bool isScalUpdate, bool isTranslateUpdate)
		{
			if (isScalUpdate is false && isTranslateUpdate is false)
			{
				return;
			}

			Matrix3x2 scale = Matrix3x2.CreateScale(ScreenZoom, ScreenZoom);
			Matrix3x2 translate = Matrix3x2.CreateTranslation(
				OffsetSize.Width, OffsetSize.Height);

			var transformMat = Matrix3x2.Multiply(scale, translate);
			_transformMatrix = new RawMatrix3x2()
			{
				M11 = transformMat.M11,
				M12 = transformMat.M12,
				M21 = transformMat.M21,
				M22 = transformMat.M22,
				M31 = transformMat.M31,
				M32 = transformMat.M32
			};

			float width = _directXView.ClientSize.Width / ScreenZoom;
			float height = _directXView.ClientSize.Height / ScreenZoom;

			var curWindowCenter = new PointF(
				_directXView.ClientSize.Width / 2,
				_directXView.ClientSize.Height / 2);

			var curProductCenter = new PointF(
				(curWindowCenter.X - OffsetSize.Width) / ScreenZoom,
				(curWindowCenter.Y - OffsetSize.Height) / ScreenZoom);

			_currentRoi.X = curProductCenter.X - width / 2;
			_currentRoi.Y = curProductCenter.Y - height / 2;
			_currentRoi.Width = width;
			_currentRoi.Height = height;

			_currentRoi.Inflate(1, 1);

			_deviceContext.Transform = _transformMatrix;
			_isUpdate = true;
		}

		private void AlignToScreenCenter()
		{
			// 화면에 맞추기 위함
			ScreenZoom = _directXView.ClientSize.Width / Roi.Width;
			if (_directXView.ClientSize.Height / Roi.Height < ScreenZoom)
			{
				ScreenZoom = _directXView.ClientSize.Height / Roi.Height;
			}

			// 줌이 적용되어있는 화면이라고 생각
			WindowPos = new PointF(
				_directXView.ClientSize.Width / 2,
				_directXView.ClientSize.Height / 2);

			// 중앙에 위치하기 위함
			ProductPos = Roi.GetCenterF();
			OffsetSize = new SizeF(
				WindowPos.X - ProductPos.X * ScreenZoom,
				WindowPos.Y - ProductPos.Y * ScreenZoom);

			_zoomedRoi = new RectangleF(
				ProductPos.X - _directXView.ClientSize.Width / 2 / ScreenZoom,
				ProductPos.Y - _directXView.ClientSize.Height / 2 / ScreenZoom,
				_directXView.ClientSize.Width / ScreenZoom,
				_directXView.ClientSize.Height / ScreenZoom);
		}

		private void ZoomInOut(bool isOut)
		{
			if (MousePressed is true)
			{
				return;
			}

			if (isOut is true)
			{
				ScreenZoom /= 1.5F;
				if (ScreenZoom <= 0.1f)
				{
					ScreenZoom = 0.1f;
				}
			}
			else
			{
				// 1.5배 확대(줌인ScreenZoom *= 1.5F;
				ScreenZoom *= 1.5F;
				if (ScreenZoom >= 1000.0f)
				{
					ScreenZoom = 1000.0f;
				}
			}

			float offsetX = _mousePos.X - ProductPos.X * ScreenZoom;
			float offsetY = _mousePos.Y - ProductPos.Y * ScreenZoom;

			OffsetSize = new SizeF(offsetX, offsetY);
			WindowPos = new PointF(_mousePos.X, _mousePos.Y);

			float productX = (WindowPos.X - OffsetSize.Width) / ScreenZoom;
			float productY = (WindowPos.Y - OffsetSize.Height) / ScreenZoom;
			ProductPos = new PointF(productX, productY);

			UpdateMatrix(true, true);

			MouseMoveEvent(this, null);
		}

		private void RenderTimer_Tick(object sender, EventArgs e)
		{
			OnDraw();
		}
	}
}
