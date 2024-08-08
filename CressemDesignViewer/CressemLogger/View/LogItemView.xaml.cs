using System.Windows;
using System.Windows.Controls;

namespace CressemLogger.View
{
	/// <summary>
	/// LogItemView.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class LogItemView : UserControl
	{
		public LogItemView()
		{
			InitializeComponent();
		}

		private void CurrentControl_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			LogList.MaxHeight = CurrentControl.ActualHeight;
			if (LogList.Items.Count > 0)
			{
				LogList.ScrollIntoView(LogList.Items[LogList.Items.Count - 1]);
			}
		}

		private void LogList_TargetUpdated(object sender, System.Windows.Data.DataTransferEventArgs e)
		{
			if (LogList.Items.Count > 0)
			{
				LogList.ScrollIntoView(LogList.Items[LogList.Items.Count - 1]);
				LogList.SelectedIndex = LogList.Items.Count - 1;
			}
		}
	}
}
