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

		private readonly List<DirectShape> _directProfileShapes = new List<DirectShape>();
		private readonly List<DirectShape> _directShapes = new List<DirectShape>();
		private DirectXWinformView _directXView = new DirectXWinformView();
		private WindowsFormsHost _directXControl;

		private Device _d3dDevice;
		private SwapChain _swapChain;
		private RenderTarget _renderTarget;
		private Factory _d2dFactory;
		private Timer _renderTimer;

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
						_d2dFactory, _renderTarget, Color.White));

					var surface = _directProfileShapes.FirstOrDefault();
					if (surface is null)
					{
						return false;
					}

					Roi = surface.GetBounds();
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

				Matrix3x2 scaleMatrix = Matrix3x2.CreateScale(ScreenZoom, ScreenZoom);
				Matrix3x2 transMatrix = Matrix3x2.CreateTranslation(
					OffsetSize.Width, OffsetSize.Height);

				var transformMat = Matrix3x2.Multiply(scaleMatrix, transMatrix);
				RawMatrix3x2 matrix = new RawMatrix3x2()
				{
					M11 = transformMat.M11,
					M12 = transformMat.M12,
					M21 = transformMat.M21,
					M22 = transformMat.M22,
					M31 = transformMat.M31,
					M32 = transformMat.M32
				};

				_renderTarget.Transform = matrix;
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
						_d2dFactory, _renderTarget, Color.DarkGreen));
				}
			}
		}

		public override void ClearShape()
		{
			if (_directShapes is null || _directProfileShapes is null ||
				_renderTarget is null || _swapChain is null)
			{
				return;
			}

			_directShapes.Clear();
			_directProfileShapes.Clear();

			_renderTarget.BeginDraw();
			_renderTarget.Clear(new RawColor4(0, 0, 0, 1));
			_renderTarget.EndDraw();

			_swapChain.Present(1, PresentFlags.None);
		}

		public override void OnDraw()
		{
			if (_renderTarget is null)
			{
				return;
			}

			_renderTarget.BeginDraw();
			_renderTarget.Clear(new RawColor4(0, 0, 0, 1));

			Matrix3x2 scaleMatrix = Matrix3x2.CreateScale(ScreenZoom, ScreenZoom);
			Matrix3x2 transMatrix = Matrix3x2.CreateTranslation(
				OffsetSize.Width, OffsetSize.Height);

			var transformMat = Matrix3x2.Multiply(scaleMatrix, transMatrix);
			RawMatrix3x2 matrix = new RawMatrix3x2()
			{
				M11 = transformMat.M11,
				M12 = transformMat.M12,
				M21 = transformMat.M21,
				M22 = transformMat.M22,
				M31 = transformMat.M31,
				M32 = transformMat.M32
			};

			_renderTarget.Transform = matrix;

			DrawShapes();

			_renderTarget.EndDraw();
			_swapChain.Present(1, PresentFlags.None);
		}

		private void OnPaint(object sender, Graphics graphics)
		{
			OnDraw();
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
		}

		private void OnResize(object sender, EventArgs e)
		{
			if (_swapChain is null)
			{
				return;
			}

			Utilities.Dispose(ref _renderTarget);
			_swapChain.ResizeBuffers(1, _directXView.ClientSize.Width, _directXView.ClientSize.Height,
				Format.R8G8B8A8_UNorm, SwapChainFlags.None);

			using (var backBuffer = _swapChain.GetBackBuffer<Texture2D>(0))
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

		private void OnMouseDoubleClick(object sender, MouseEventArgs e)
		{
		}

		private void OnMouseDown(object sender, MouseEventArgs e)
		{
			MousePressed = true;

			StartPos = new PointF(OffsetSize.Width, OffsetSize.Height);
			WindowPos = new PointF(e.X, e.Y);
		}

		private void OnMouseMove(object sender, MouseEventArgs e)
		{
			MousePos = new PointF(
				(e.X - OffsetSize.Width) / ScreenZoom - Roi.X, 
				(e.Y - OffsetSize.Height) / ScreenZoom - Roi.Y);

			if (MousePressed && e.Button is MouseButtons.Left)
			{
				float deltaX = e.X - WindowPos.X;
				float deltaY = e.Y - WindowPos.Y;

				OffsetSize = new SizeF(StartPos.X + deltaX, StartPos.Y + deltaY);
			}

			MouseMoveEvent(this, null);
		}

		private void OnMouseUp(object sender, MouseEventArgs e)
		{
			if (MousePressed && e.Button is MouseButtons.Left)
			{
				WindowPos = new PointF(e.X, e.Y);

				float productX = (WindowPos.X - OffsetSize.Width) / ScreenZoom;
				float productY = (WindowPos.Y - OffsetSize.Height) / ScreenZoom;

				ProductPos = new PointF(productX, productY);
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

				WindowPos = new PointF(
					_directXView.ClientSize.Width / 2,
					_directXView.ClientSize.Height / 2);

				// 중앙에 위치하기 위함
				ProductPos = Roi.GetCenterF();
				OffsetSize = new SizeF(WindowPos.X, WindowPos.Y);
			}
		}

		private void ResetView()
		{
			_directXView.GraphicsPaint -= OnPaint;
			_directXView.GraphicsMouseWheel -= OnMouseWheel;
			_directXView.GraphicsResize -= OnResize;
			_directXView.GraphicsMouseDoubleClick -= OnMouseDoubleClick;
			_directXView.GraphicsMouseDown -= OnMouseDown;
			_directXView.GraphicsMouseMove -= OnMouseMove;
			_directXView.GraphicsMouseUp -= OnMouseUp;
			_directXView.GraphicsPrevKeyDown -= OnPrevkeyDown;

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

				shape.Draw(_renderTarget);
			}

			foreach (var shape in _directShapes)
			{
				if (shape is null)
				{
					continue;
				}

				shape.Fill(_renderTarget, false);
			}
		}

		private void RenderStart()
		{
			SwapChainDescription swapChainDesc = new SwapChainDescription()
			{
				BufferCount = 1,
				ModeDescription = new ModeDescription(
					_directXView.ClientSize.Width, _directXView.ClientSize.Height,
					new Rational(30, 1), Format.R8G8B8A8_UNorm),
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
			{
				using (var surface = backBuffer.QueryInterface<Surface>())
				{
					RenderTargetProperties renderTargetProperties = new RenderTargetProperties(
						new PixelFormat(Format.Unknown, SharpDX.Direct2D1.AlphaMode.Premultiplied));

					_renderTarget = new RenderTarget(_d2dFactory, surface, renderTargetProperties);
				}
			}

			_renderTimer = new Timer { Interval = 30 }; // 약 30FPS
			_renderTimer.Tick += RenderTimer_Tick;
			_renderTimer.Start();
		}

		private void RenderTimer_Tick(object sender, EventArgs e)
		{
			OnDraw();
		}
	}
}
