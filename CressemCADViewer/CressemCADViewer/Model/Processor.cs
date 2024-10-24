using System;
using System.Threading.Tasks;
using CressemExtractLibrary;
using CressemExtractLibrary.Data;

namespace CressemCADViewer.Model
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
			bool result = false;
			_task = Task.Run(() =>
			{
				if (ExtractLibrary.Instance.SetData(format, loadPath, savePath) is true)
				{
					ProcessStarted(this, true);

					if (ExtractLibrary.Instance.Extract() is true)
					{
						result = true;
					}
					else
					{
						result = false;
					}
				}
				else
				{
					result = false;
				}
			});

			_task.Wait();
			ProcessCompleted(this, result);
		}
	}
}
