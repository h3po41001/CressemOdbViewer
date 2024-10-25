using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using ImageControl.Gdi.View;
using ImageControl.Shape.DirectX;
using ImageControl.Shape.DirectX.Interface;
using ImageControl.Shape.Gdi.Interface;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;
using Device = SharpDX.Direct3D11.Device;
using Factory = SharpDX.Direct2D1.Factory;

namespace ImageControl.Model.DirectX
{
	internal class DirectXGraphics : SmartGraphics
	{
		public override event EventHandler MouseMoveEvent = delegate { };

		private readonly DirectXWinformView _directXView = new DirectXWinformView();
		private readonly List<DirectShape> _directProfileShapes = new List<DirectShape>();
		private readonly List<DirectShape> _directShapes = new List<DirectShape>();
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
			using (var backBuffer = _swapChain.GetBackBuffer<Texture2D>(0))
			using (var surface = backBuffer.QueryInterface<Surface>())
			{
				RenderTargetProperties renderTargetProperties = new RenderTargetProperties(
					new PixelFormat(Format.Unknown, SharpDX.Direct2D1.AlphaMode.Premultiplied));

				_renderTarget = new RenderTarget(_d2dFactory, surface, renderTargetProperties);
			}

			_renderTimer = new Timer
			{
				Interval = 30 // 약 30FPS
			};

			_directShapes.Add(new DirectArc(100, 100, 200, 200, 100, 100, 0, true, false, _d2dFactory, _renderTarget, Color.Red));
			_directShapes.Add(new DirectEllipse(120.8f, 96.5f, 31.5f, 19.2f, _d2dFactory, _renderTarget, Color.Red));

			_renderTimer.Tick += RenderTimer_Tick;
			_renderTimer.Start();

			_directXView.GraphicsPaint += OnPaint;
			_directXView.GraphicsMouseWheel += OnMouseWheel;
			_directXView.GraphicsResize += OnResize;
			_directXView.GraphicsMouseDoubleClick += OnMouseDoubleClick;
			_directXView.GraphicsMouseDown += OnMouseDown;
			_directXView.GraphicsMouseMove += OnMouseMove;
			_directXView.GraphicsMouseUp += OnMouseUp;
			_directXView.GraphicsPrevKeyDown += OnPrevkeyDown;
		}		

		public override bool LoadProfile(object profileShape)
		{
			return true;
		}

		public override void AddShapes(object shape)
		{
			if (shape is IDirectList directList)
			{
				foreach (IDirectShape directShape in directList.Shapes)
				{
					if (directShape is null)
					{
						continue;
					}

					_directShapes.Add(DirectShapeFactory.Instance.CreateDirectShape(
						directShape, _d2dFactory, _renderTarget, Color.Red));
				}
			}
		}

		public override void ClearShape()
		{
		}

		public override void OnDraw()
		{
			_renderTarget.BeginDraw();
			_renderTarget.Clear(new RawColor4(0, 0, 0, 1));

			foreach (var shape in _directShapes)
			{
				if (shape is null)
				{
					continue;
				}

				shape.Fill(_renderTarget);
			}

			_renderTarget.EndDraw();
			_swapChain.Present(1, PresentFlags.None);
		}

		private void OnPaint(object sender, Graphics graphics)
		{
			OnDraw();
		}		

		private void OnMouseWheel(object sender, MouseEventArgs arg)
		{
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

		private void OnMouseDoubleClick(object sender, MouseEventArgs arg)
		{
		}

		private void OnMouseDown(object sender, MouseEventArgs arg)
		{
		}

		private void OnMouseMove(object sender, MouseEventArgs arg)
		{
		}

		private void OnMouseUp(object sender, MouseEventArgs arg)
		{
		}

		private void OnPrevkeyDown(object sender, PreviewKeyDownEventArgs arg)
		{
		}

		private void RenderTimer_Tick(object sender, EventArgs e)
		{
			OnDraw();
		}
	}
}
