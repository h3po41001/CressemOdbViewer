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
using ImageControl.Shape.DirectX.Interface;
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

		private readonly List<DirectShape> _directProfileShapes = new List<DirectShape>();
		private readonly List<DirectShape> _directShapes = new List<DirectShape>();
		private DirectXWinformView _directXView = new DirectXWinformView();
		private WindowsFormsHost _directXControl;

		private Device _d3dDevice;
		private SwapChain _swapChain;
		//private RenderTarget _renderTarget;
		SharpDX.DXGI.Device _dxgiDevice;
		SharpDX.Direct2D1.Device1 _d2dDevice;
		private DeviceContext _deviceContext;

		private LayerParameters _layerOption;
		private Bitmap1 _drawingLayer;
		private Bitmap1 _shapeLayer;
		private Bitmap1 _renderBitmap;

		private SharpDX.Direct2D1.Factory2 _d2dFactory;
		private Timer _renderTimer;

		private bool _zoomMousePressed = false;
		private bool _zoomIsReady = false;
		private PointF _zoomMousePos = new PointF();
		private float _zoomLineWidth = 1f;
		private RectangleF _zoomedRoi = new RectangleF();
		private SolidColorBrush _zoomBrush;

		private bool _isUpdate = false;
		private RawMatrix3x2 _transformMatrix = new RawMatrix3x2();
		private RectangleF _currentRoi = new RectangleF();

		public override void Initialize()
		{
			ResetView();

			_directXControl = GraphicsControl as WindowsFormsHost;
			_directXControl.Child = _directXView;

			RenderStart();

			_directXView.GraphicsPaint += OnPaint;
			_directXView.GraphicsMouseWheel += OnMouseWheel;
			_directXView.GraphicsResize += OnResize;
			_directXView.GraphicsMouseDoubleClick += OnMouseDoubleClick;
			_directXView.GraphicsMouseDown += OnMouseDown;
			_directXView.GraphicsMouseMove += OnMouseMove;
			_directXView.GraphicsMouseUp += OnMouseUp;
			_directXView.GraphicsPrevKeyDown += OnPrevkeyDown;
		}

		public override bool LoadProfile(IGraphicsList profileShapes)
		{
			if (profileShapes is null)
			{
				return false;
			}

			if (profileShapes is IDirectList directList)
			{
				foreach (var shape in directList.Shapes)
				{
					_directProfileShapes.Add(DirectShapeFactory.Instance.CreateDirectShape(
						directList.IsPositive, (dynamic)shape,
						_d2dFactory, _deviceContext, Color.White));

					var surface = _directProfileShapes.FirstOrDefault();
					if (surface is null)
					{
						return false;
					}

					Roi = surface.Bounds;
				}

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

				UpdateMatrix(true, true);
			}

			return true;
		}

		public override void AddShapes(IGraphicsList shapes)
		{
			if (shapes is null)
			{
				return;
			}

			if (shapes is IDirectList directList)
			{
				foreach (var shape in directList.Shapes)
				{
					if (shape is null)
					{
						continue;
					}

					_directShapes.Add(DirectShapeFactory.Instance.CreateDirectShape(
						directList.IsPositive, (dynamic)shape,
						_d2dFactory, _deviceContext, Color.DarkGreen));
				}
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

			//_renderTarget.BeginDraw();
			//_renderTarget.Clear(new RawColor4(0, 0, 0, 1));
			//_renderTarget.EndDraw();

			//_swapChain.Present(1, PresentFlags.None);
		}

		public override void OnDraw()
		{
			if (_deviceContext is null)
			{
				return;
			}

			if (_zoomMousePressed is true)
			{
				_drawingLayer.Tag = null;
				_deviceContext.Target = _drawingLayer;

				_deviceContext.BeginDraw();
				_deviceContext.Clear(new RawColor4(0, 0, 0, 1));

				_deviceContext.DrawRectangle(new RawRectangleF(
					_zoomMousePos.X, _zoomMousePos.Y, ProductPos.X, ProductPos.Y),
					_zoomBrush, _zoomLineWidth);

				_deviceContext.EndDraw();
				_drawingLayer.Tag = "Done";

				_deviceContext.Target = _renderBitmap;
			}

			if (_isUpdate is true)
			{
				ShapeRenderStart();
				_isUpdate = false;
			}

			_deviceContext.BeginDraw();
			
			_deviceContext.Clear(new RawColor4(0, 0, 0, 1));
			_deviceContext.DrawBitmap(_drawingLayer, new RawRectangleF(Roi.Left, Roi.Top, Roi.Right, Roi.Bottom), 1, 
				BitmapInterpolationMode.NearestNeighbor);
			DrawShapes();
			_deviceContext.EndDraw();

			_swapChain.Present(1, PresentFlags.None);
		}

		private void OnPaint(object sender, Graphics graphics)
		{
			//OnDraw();
		}

		private void OnMouseWheel(object sender, MouseEventArgs e)
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
			else if (ScreenZoom >= 1000.0f)
			{
				ScreenZoom = 1000.0f;
			}

			float offsetX = e.X - ProductPos.X * ScreenZoom;
			float offsetY = e.Y - ProductPos.Y * ScreenZoom;

			OffsetSize = new SizeF(offsetX, offsetY);
			WindowPos = new PointF(e.X, e.Y);

			float productX = (WindowPos.X - OffsetSize.Width) / ScreenZoom;
			float productY = (WindowPos.Y - OffsetSize.Height) / ScreenZoom;
			ProductPos = new PointF(productX, productY);

			UpdateMatrix(true, true);

			MouseMoveEvent(this, null);
		}

		private void OnResize(object sender, EventArgs e)
		{
			if (_swapChain is null)
			{
				return;
			}

			Utilities.Dispose(ref _zoomBrush);
			Utilities.Dispose(ref _drawingLayer);
			Utilities.Dispose(ref _shapeLayer);
			Utilities.Dispose(ref _renderBitmap);
			Utilities.Dispose(ref _deviceContext);

			_swapChain.ResizeBuffers(1, _directXView.ClientSize.Width, _directXView.ClientSize.Height,
				Format.R8G8B8A8_UNorm, SwapChainFlags.None);

			// 백 버퍼로부터 RenderTarget 생성
			//using (var backBuffer = _swapChain.GetBackBuffer<SharpDX.Direct3D11.Texture2D>(0))
			//{
			//	using (var surface = backBuffer.QueryInterface<Surface>())
			//	{
			//		RenderTargetProperties renderTargetProperties = new RenderTargetProperties(
			//			new PixelFormat(Format.R8G8B8A8_UNorm, SharpDX.Direct2D1.AlphaMode.Premultiplied));

			//		_renderTarget = new RenderTarget(_d2dFactory, surface, renderTargetProperties);
			//	}
			//}

			InitRender();

			_zoomBrush = new SolidColorBrush(_deviceContext, new RawColor4(1, 1, 1, 1));
			_directXView.Invalidate();
		}

		private void OnMouseDoubleClick(object sender, MouseEventArgs e)
		{
		}

		private void OnMouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button is MouseButtons.Right)
			{
				MousePressed = true;
			}
			else if (e.Button is MouseButtons.Left)
			{
				if (_zoomIsReady is true)
				{
					RectangleF roi = new RectangleF()
					{
						X = _zoomMousePos.X,
						Y = _zoomMousePos.Y,
						Width = ProductPos.X - _zoomMousePos.X,
						Height = ProductPos.Y - _zoomMousePos.Y,
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
					_zoomMousePos = new PointF(productX, productY);

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
			MousePos = new PointF(
				(e.X - OffsetSize.Width) / ScreenZoom,
				(e.Y - OffsetSize.Height) / ScreenZoom);

			float productX = (e.X - OffsetSize.Width) / ScreenZoom;
			float productY = (e.Y - OffsetSize.Height) / ScreenZoom;
			ProductPos = new PointF(productX, productY);

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

		private void OnPrevkeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			if (e.KeyCode is Keys.Home)
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

				UpdateMatrix(true, true);
			}
			else if (e.KeyCode is Keys.PageUp)
			{
				UpdateMatrix(true, true);
			}
		}

		private void ResetView()
		{
			if (_renderTimer != null)
			{
				_renderTimer.Stop();
				_renderTimer.Dispose();
			}

			_directXView.GraphicsPaint -= OnPaint;
			_directXView.GraphicsMouseWheel -= OnMouseWheel;
			_directXView.GraphicsResize -= OnResize;
			_directXView.GraphicsMouseDoubleClick -= OnMouseDoubleClick;
			_directXView.GraphicsMouseDown -= OnMouseDown;
			_directXView.GraphicsMouseMove -= OnMouseMove;
			_directXView.GraphicsMouseUp -= OnMouseUp;
			_directXView.GraphicsPrevKeyDown -= OnPrevkeyDown;

			Utilities.Dispose(ref _deviceContext);
			Utilities.Dispose(ref _zoomBrush);
			Utilities.Dispose(ref _d3dDevice);
			Utilities.Dispose(ref _swapChain);
			Utilities.Dispose(ref _d2dFactory);

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

				shape.Draw(_deviceContext, _currentRoi);
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
					new Rational(16, 1), Format.R8G8B8A8_UNorm),
				IsWindowed = true,
				OutputHandle = _directXView.Handle,
				SampleDescription = new SampleDescription(1, 0),
				SwapEffect = SwapEffect.Discard,
				Usage = Usage.RenderTargetOutput
			};

			// Direct3D11 장치 및 SwapChain 생성
			Device.CreateWithSwapChain(SharpDX.Direct3D.DriverType.Hardware,
				SharpDX.Direct3D11.DeviceCreationFlags.BgraSupport,
				swapChainDesc,
				out _d3dDevice,
				out _swapChain);

			// Direct2D Factory 생성
			_d2dFactory = new SharpDX.Direct2D1.Factory2(FactoryType.SingleThreaded, DebugLevel.None);
			_dxgiDevice = _d3dDevice.QueryInterface<SharpDX.DXGI.Device>();
			_d2dDevice = new SharpDX.Direct2D1.Device1(_d2dFactory, _dxgiDevice);

			// 백 버퍼로부터 RenderTarget 생성
			//using (var backBuffer = _swapChain.GetBackBuffer<SharpDX.Direct3D11.Texture2D>(0))
			//{
			//	using (var surface = backBuffer.QueryInterface<Surface>())
			//	{
			//		RenderTargetProperties renderTargetProperties = new RenderTargetProperties(
			//			new PixelFormat(Format.Unknown, SharpDX.Direct2D1.AlphaMode.Premultiplied));

			//		_renderTarget = new RenderTarget(_d2dFactory, surface, renderTargetProperties);
			//	}
			//}

			InitRender();

			_renderTimer = new Timer { Interval = 16 }; // 약 10FPS
			_renderTimer.Tick += RenderTimer_Tick;
			_renderTimer.Start();
		}

		private void InitRender()
		{
			_deviceContext = new DeviceContext1(_d2dDevice, DeviceContextOptions.None);

			using (var backBuffer = _swapChain.GetBackBuffer<SharpDX.Direct3D11.Texture2D>(0))
			{
				using (var surface = backBuffer.QueryInterface<Surface>())
				{
					BitmapProperties1 bitmapProperties = new BitmapProperties1(new PixelFormat(
						Format.R8G8B8A8_UNorm, SharpDX.Direct2D1.AlphaMode.Premultiplied),
						DEFAULT_DPI, DEFAULT_DPI, BitmapOptions.Target | BitmapOptions.CannotDraw);

					// RenderTarget 설정
					_renderBitmap = new Bitmap1(_deviceContext, surface, bitmapProperties);
					_deviceContext.Target = _renderBitmap;
				}
			}

			BitmapProperties1 prop = new BitmapProperties1(new PixelFormat(
				Format.R8G8B8A8_UNorm, SharpDX.Direct2D1.AlphaMode.Premultiplied),
				DEFAULT_DPI, DEFAULT_DPI, BitmapOptions.Target);

			_drawingLayer = new Bitmap1(_deviceContext,
				new Size2(_directXView.ClientSize.Width, _directXView.ClientSize.Height), prop);

			_shapeLayer = new Bitmap1(_deviceContext,
				new Size2(_directXView.ClientSize.Width, _directXView.ClientSize.Height), prop);

			_directXView.Invalidate();
			_zoomBrush = new SolidColorBrush(_deviceContext, new RawColor4(1, 1, 1, 1));

			_layerOption = new LayerParameters()
			{
				ContentBounds = new RawRectangleF(Roi.Left, Roi.Top, Roi.Right, Roi.Bottom),
				GeometricMask = null,
				MaskAntialiasMode = AntialiasMode.PerPrimitive,
				MaskTransform = _transformMatrix,//new RawMatrix3x2(1, 0, 0, 1, 0, 0),
				Opacity = 1,
				OpacityBrush = null,
				LayerOptions = LayerOptions.None,
			};
		}

		private void ShapeRenderStart()
		{
			_shapeLayer.Tag = null;

			_deviceContext.Target = _shapeLayer;			
			//_deviceContext.Transform = _transformMatrix;

			_deviceContext.BeginDraw();
			//_deviceContext.PushLayer(ref _layerOption, null);

			_deviceContext.Clear(new RawColor4(0, 0, 0, 1));
			//_deviceContext.FillRectangle(
			//	new RawRectangleF(0, 0, _directXView.ClientSize.Width, _directXView.ClientSize.Height), _zoomBrush);

			DrawShapes();

			//_deviceContext.PopLayer();
			_deviceContext.EndDraw();
			_shapeLayer.Tag = "Done";

			_deviceContext.Target = _renderBitmap;
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

			_deviceContext.Target = _renderBitmap;
			_deviceContext.Transform = _transformMatrix;// new RawMatrix3x2(1, 0, 0, 1, 0, 0);

			_drawingLayer.Tag = null;
			_shapeLayer.Tag = null;

			_isUpdate = true;
		}

		private void RenderTimer_Tick(object sender, EventArgs e)
		{
			OnDraw();
		}
	}
}
