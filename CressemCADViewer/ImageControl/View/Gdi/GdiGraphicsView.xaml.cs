using System.Windows.Controls;
using ImageControl.Model;

namespace ImageControl.View.Gdi
{
	/// <summary>
	/// GdiGraphicsView.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class GdiGraphicsView : UserControl
	{
		public GdiGraphicsView()
		{
			InitializeComponent();
		}

		private void GraphicsControl_DataContextChanged(object sender,
			System.Windows.DependencyPropertyChangedEventArgs e)
		{
			if (DataContext is SmartGraphics vm)
			{
				vm.GraphicsControl = graphicsControl;
			}
		}
	}
}
