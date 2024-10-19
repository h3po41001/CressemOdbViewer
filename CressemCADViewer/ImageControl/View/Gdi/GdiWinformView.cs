using System;
using System.Drawing;
using System.Windows.Forms;
using ImageControl.Model.Event;

namespace ImageControl.Gdi.View
{
	public partial class GdiWinformView : UserControl
	{
		public event GraphicsEventHandler<Graphics> GraphicsPaint = delegate { };
		public event GraphicsEventHandler<MouseEventArgs> GraphicsMouseWheel = delegate { };
		public event GraphicsEventHandler<EventArgs> GraphicsResize = delegate { };
		public event GraphicsEventHandler<MouseEventArgs> GraphicsMouseDoubleClick = delegate { };
		public event GraphicsEventHandler<MouseEventArgs> GraphicsMouseDown = delegate { };
		public event GraphicsEventHandler<MouseEventArgs> GraphicsMouseMove = delegate { };
		public event GraphicsEventHandler<MouseEventArgs> GraphicsMouseUp = delegate { };
		public event GraphicsEventHandler<PreviewKeyDownEventArgs> GraphicsPrevKeyDown = delegate { };
		public event GraphicsEventHandler<KeyEventArgs> GraphicsKeyUp = delegate { };
		public GdiWinformView()
		{
			InitializeComponent();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			GraphicsPaint(this, e.Graphics);
		}

		protected override void OnMouseWheel(MouseEventArgs e)
		{
			base.OnMouseWheel(e);
			GraphicsMouseWheel(this, e);
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			GraphicsResize(this, e);
		}

		protected override void OnMouseDoubleClick(MouseEventArgs e)
		{
			base.OnMouseDoubleClick(e);
			GraphicsMouseDoubleClick(this, e);
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			GraphicsMouseDown(this, e);
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			GraphicsMouseMove(this, e);
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			GraphicsMouseUp(this, e);
		}

		protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
		{
			base.OnPreviewKeyDown(e);
			GraphicsPrevKeyDown(this, e);
		}

		protected override void OnKeyUp(KeyEventArgs e)
		{
			base.OnKeyUp(e);
			GraphicsKeyUp(this, e);
		}
	}
}
