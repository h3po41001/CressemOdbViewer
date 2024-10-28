using System.Windows;

namespace CressemCADViewer.View.Control
{
	/// <summary>
	/// SelectGraphicsView.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class SelectGraphicsView : Window
	{
		public SelectGraphicsView()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
			Close();
        }
    }
}
