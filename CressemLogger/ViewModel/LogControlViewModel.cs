using CressemFramework.Observer;
using CressemLogger.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Threading;

namespace CressemLogger.ViewModel
{
	public class LogControlViewModel : ObservableObject
	{
		private readonly Thread _logThread = null;
		private readonly CancellationTokenSource _cancellationToken = new CancellationTokenSource();

		private readonly Dictionary<string, LogItemViewModel> _logViewList = new Dictionary<string, LogItemViewModel>();
		private LogItemViewModel _selectedLogView = null;

		private readonly ConcurrentQueue<LogDisplay> _logMessagesQueue = new ConcurrentQueue<LogDisplay>();
		private readonly Dispatcher _logDispatcher;

		public LogControlViewModel(Dispatcher dispatcher)
		{
			_logDispatcher = dispatcher;
			_logThread = new Thread(WriteLog) { IsBackground = true };
			_logThread.Start();

			_logDispatcher.ShutdownStarted += Dispose;
		}

		public IEnumerable<LogItemViewModel> LogViewList
		{
			get => _logViewList.Select(x => x.Value);
		}

		public LogItemViewModel SelectedLogView
		{
			get => _selectedLogView;
			set
			{
				_selectedLogView = value;
				OnPropertyChanged();
			}
		}

		public void AddLogClass(string logClass)
		{
			if (_logViewList.ContainsKey(logClass) is false)
			{
				_logViewList.Add(logClass, new LogItemViewModel() { Name = logClass });
				CLogger.Instance.AddLogClass(logClass, UpdateLog);
				OnPropertyChanged("LogViewList");
			}
		}

		public void Referesh()
		{
			if (_logViewList != null)
			{
				SelectedLogView = _logViewList["Main"];
			}
			else
			{
				OnPropertyChanged("SelectedLogView");
			}
		}

		private void UpdateLog(string className, string message, Color color)
		{
			_logMessagesQueue.Enqueue(new LogDisplay(className, message, color));
		}

		private void WriteLog()
		{
			while (true)
			{
				if (_cancellationToken.IsCancellationRequested is true)
					break;

				_logDispatcher.InvokeAsync(new Action(() =>
				{
					while (_logMessagesQueue.TryDequeue(out LogDisplay output) is true)
					{
						_logViewList[output.Name].AddLog(output);
					}

					Thread.Sleep(100);

				}));

				Thread.Sleep(500);
			}
		}

		public void Dispose(object obj, EventArgs args)
		{
			_cancellationToken.Cancel();
		}
	}
}
