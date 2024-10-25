using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms.Integration;
using ImageControl.Gdi.View;
using ImageControl.Model.Shape.Gdi;
using ImageControl.Shape.Interface;
using System.Windows.Forms;

using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DXGI;
using SharpDX.Direct3D11;
using SharpDX.Direct3D;
using SharpDX.Mathematics.Interop;
using Device = SharpDX.Direct3D11.Device;
using Factory = SharpDX.Direct2D1.Factory;

namespace ImageControl.Model.DirectX
{
	internal class DirectXGraphics : SmartGraphics
	{
		public override event EventHandler MouseMoveEvent = delegate { };

		private readonly DirectXWinformView _directXView = new DirectXWinformView();
		private readonly List<GdiShape> _gdiProfileShapes = new List<GdiShape>();
		private readonly List<GdiShape> _gdiShapes = new List<GdiShape>();
		private WindowsFormsHost _directXControl;

		private Device _d3dDevice;
		private SwapChain _swapChain;
		private RenderTarget _renderTarget;
		private Factory _d2dFactory;
		private Timer _renderTimer;

		public override void Initialize()
		{
			_directXControl = GraphicsControl as WindowsFormsHost;
			_directXControl.Child = _directXView;

			SwapChainDescription swapChainDesc = new SwapChainDescription()
			{
				BufferCount = 1,
				ModeDescription =
				new ModeDescription(_directXView.ClientSize.Width, _directXView.ClientSize.Height,
				new Rational(60, 1), Format.R8G8B8A8_UNorm),
				IsWindowed = true,
				OutputHandle = _directXView.Handle,
				SampleDescription = new SampleDescription(1, 0),
				SwapEffect = SwapEffect.Discard,
				Usage = Usage.RenderTargetOutput
			};

			// Direct3D11 장치 및 SwapChain 생성
			Device.CreateWithSwapChain(DriverType.Hardware,
				DeviceCreationFlags.BgraSupport,
				swapChainDesc,
				out _d3dDevice,
				out _swapChain);

			// Direct2D Factory 생성
			_d2dFactory = new Factory();

			// 백 버퍼로부터 RenderTarget 생성
			using (var backBuffer = _swapChain.GetBackBuffer<SharpDX.Direct3D11.Texture2D>(0))
			using (var surface = backBuffer.QueryInterface<Surface>())
			{
				RenderTargetProperties renderTargetProperties = new RenderTargetProperties(
					new PixelFormat(Format.Unknown, SharpDX.Direct2D1.AlphaMode.Premultiplied));

				_renderTarget = new RenderTarget(_d2dFactory, surface, renderTargetProperties);
			}

			_renderTimer = new Timer
			{
				Interval = 30 // 약 60FPS
			};

			_renderTimer.Tick += RenderTimer_Tick;
			_renderTimer.Start();

			_directXView.GraphicsPaint += OnPaint;
			//_directXView.GraphicsMouseWheel += GdiMouseWheel;
			_directXView.GraphicsResize += OnResize;
			//_directXView.GraphicsMouseDoubleClick += GdiMouseDoubleClick;
			//_directXView.GraphicsMouseDown += GdiMouseDown;
			//_directXView.GraphicsMouseMove += GdiMouseMove;
			//_directXView.GraphicsMouseUp += GdiMouseUp;
			//_directXView.GraphicsPrevKeyDown += GdiPrevkeyDown;
		}

		public override bool LoadProfile(IShapeList profileShape)
		{
			return true;
		}

		public override void AddShapes(IShapeList shape)
		{
		}

		public override void ClearShape()
		{
		}

		public override void OnDraw()
		{
			_renderTarget.BeginDraw();
			_renderTarget.Clear(new RawColor4(0, 0, 0, 1));

			// 폴리곤 그리기
			using (SolidColorBrush brush = new SolidColorBrush(_renderTarget, new RawColor4(0, 1, 0, 1)))
			{
				RawVector2 startPoint = new RawVector2(300.5f, 300.5f); // 아크의 시작점
				RawVector2 endPoint = new RawVector2(500.5f, 500.5f);   // 아크의 끝점

				float radiusX = 1f; // x축 반지름
				float radiusY = 1f; // y축 반지름

				float rotationAngle = 0; // 회전 각도 (도 단위)
				ArcSize arcSize = ArcSize.Small; // 아크의 크기 (Small 또는 Large)
				SweepDirection sweepDirection = SweepDirection.Clockwise;

				// 경로 그리기
				using (var pathGeometry = new PathGeometry(_d2dFactory))
				{
					using (GeometrySink sink = pathGeometry.Open())
					{
						sink.BeginFigure(startPoint, FigureBegin.Filled);

						// ArcSegment 생성
						ArcSegment arcSegment = new ArcSegment()
						{
							Point = endPoint,
							Size = new Size2F(radiusX, radiusY),
							RotationAngle = rotationAngle,
							ArcSize = arcSize,
							SweepDirection = sweepDirection
						};

						// 아크 세그먼트 추가
						sink.AddArc(arcSegment);

						// 도형 종료
						sink.EndFigure(FigureEnd.Open);
						sink.Close();
					}


					_renderTarget.FillGeometry(pathGeometry, brush);
				}
			}

			_renderTarget.EndDraw();
			_swapChain.Present(1, PresentFlags.None);
		}

		private void OnPaint(object sender, Graphics graphics)
		{
			OnDraw();
		}

		private void OnResize(object sender, EventArgs arg)
		{
			if (_swapChain is null)
			{
				return;
			}

			Utilities.Dispose(ref _renderTarget);
			_swapChain.ResizeBuffers(1, _directXView.ClientSize.Width, _directXView.ClientSize.Height, Format.R8G8B8A8_UNorm, SwapChainFlags.None);

			using (var backBuffer = _swapChain.GetBackBuffer<SharpDX.Direct3D11.Texture2D>(0))
			{
				using (var surface = backBuffer.QueryInterface<Surface>())
				{
					RenderTargetProperties renderTargetProperties = new RenderTargetProperties(
						new PixelFormat(Format.Unknown, SharpDX.Direct2D1.AlphaMode.Premultiplied));

					_renderTarget = new RenderTarget(_d2dFactory, surface, renderTargetProperties);
					_directXView.Invalidate();
				}
			}
		}

		private void RenderTimer_Tick(object sender, EventArgs e)
		{
			// 화면 갱신 요청
			OnDraw();
		}
	}
}
