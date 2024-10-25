using System.Windows;
using System.Windows.Controls;
using ImageControl.Model;

namespace ImageControl.View.DirectX
{
	/// <summary>
	/// DirectXView.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class DirectXView : UserControl
	{
		public DirectXView()
		{
			InitializeComponent();
		}

		private void GraphicsControl_DataContextChanged(object sender, 
			DependencyPropertyChangedEventArgs e)
		{
			if (DataContext is SmartGraphics vm)
			{
				vm.GraphicsControl = graphicsControl;
			}
		}
    }
}
