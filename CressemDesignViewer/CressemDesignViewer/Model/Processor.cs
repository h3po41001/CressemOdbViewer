using System;
using System.Threading.Tasks;
using CressemExtractLibrary;
using CressemExtractLibrary.Data;

namespace CressemDesignViewer.Model
{
	public class Processor
	{
		public event EventHandler<bool> ProcessStarted = delegate { };
		public event EventHandler<bool> ProcessCompleted = delegate { };

		private Task _task = null;

		public Processor()
		{
		}

		public void Run(DesignFormat format, string loadPath, string savePath)
		{
			if (_task != null && _task.Status is TaskStatus.Running)
				return;

			_task = Task.Run(() =>
			{
				if (ExtractLibrary.Instance.SetData(format, loadPath, savePath) is true)
				{
					ProcessStarted(this, true);

					if (ExtractLibrary.Instance.Extract() is true)
					{
						ProcessCompleted(this, true);
					}
					else
					{
						ProcessCompleted(this, false);
					}
				}
				else
				{
					ProcessStarted(this, false);
				}
			});
		}
	}
}
