using CressemLogger.Event;
using CressemLogger.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CressemLogger.Factory
{
	internal class LogManager
	{
		private LogFactory _factory;

		private Task _logThread = null;
		private readonly CancellationTokenSource _cancellationToken = new CancellationTokenSource();

		private readonly ConcurrentQueue<LogData> _logMessages = new ConcurrentQueue<LogData>();

		private string _logFolderPath = string.Empty;
		private string _logFileName = string.Empty;

		private StreamWriter _totalLogWriter;
		private Dictionary<LogType, StreamWriter> _typeLogWriter;

		public LogManager()
		{
		}

		public void Initialize(string folderPath)
		{
			_logFolderPath = folderPath;
			Directory.CreateDirectory(_logFolderPath);

			_logFileName = $"{DateTime.Now:yyyy-MM-dd HH-mm-ss}";
			string filePath = Path.Combine(_logFolderPath, _logFileName);

			_totalLogWriter = new StreamWriter(filePath + ".log", true);
			_typeLogWriter = new Dictionary<LogType, StreamWriter>
			{
				{ LogType.Info, new StreamWriter(filePath + $".{LogType.Info}", true) },
				{ LogType.Warn, new StreamWriter(filePath + $".{LogType.Warn}", true) },
				{ LogType.Err, new StreamWriter(filePath + $".{LogType.Err}", true) },
				{ LogType.Debug, new StreamWriter(filePath + $".{LogType.Debug}", true) }
			};

			_factory = new LogFactory(_logFolderPath, _logFileName);
		}

		public bool AddClass(string className, LoggerEvent logEvent)
		{
			return _factory.AddClass(className, logEvent);
		}

		public void LogStart()
		{
			_logThread = Task.Run(WriteLog);
		}

		public bool AddInfoLog(string className, string log, bool display)
		{
			if (_factory.IsExist(className) is false)
				return false;

			_logMessages.Enqueue(new LogData(LogType.Info, className, log, Color.White, display));

			return true;
		}

		public bool AddWarningLog(string className, string log, bool display)
		{
			if (_factory.IsExist(className) is false)
				return false;

			_logMessages.Enqueue(new LogData(LogType.Warn, className, log, Color.Yellow, display));

			return true;
		}

		public bool AddErrorLog(string className, string log, bool display = false)
		{
			if (_factory.IsExist(className) is false)
				return false;

			_logMessages.Enqueue(new LogData(LogType.Err, className, log, Color.Red, display));

			return true;
		}

		public bool AddDebugLog(string className, string log, bool display)
		{
			if (_factory.IsExist(className) is false)
				return false;

			_logMessages.Enqueue(new LogData(LogType.Debug, className, log, Color.Blue, display));

			return true;
		}

		public bool AddPointLog(string className, string log, bool display)
		{
			if (_factory.IsExist(className) is false)
				return false;

			_logMessages.Enqueue(new LogData(LogType.Info, className, log, Color.GreenYellow, display));

			return true;
		}

		public bool AddLogBroadcast(string log, bool display)
		{
			var logClasses = _factory.GetLogClasses();
			if (logClasses.Count() == 0)
				return false;

			foreach (var logClass in logClasses)
			{
				_logMessages.Enqueue(new LogData(LogType.Info, logClass.Name, log, Color.White, display));
			}

			return true;
		}

		private void WriteLog()
		{
			while (true)
			{
				if (_cancellationToken.IsCancellationRequested is true)
					break;

				if (_factory is null)
					continue;

				if (_logMessages.Count > 0)
				{
					while (_logMessages.Count > 0)
					{
						if (_logMessages.TryDequeue(out var logData) is true)
						{
							string date = $"[{DateTime.Now:yyyy-MM-dd}]";

							_totalLogWriter.WriteLine(date + logData.Log);
							_typeLogWriter[logData.LogType].WriteLine(date + logData.Log);

							_factory.WriteLog(logData);
						}
					}

					_totalLogWriter.Flush();

					foreach (var writer in _typeLogWriter)
						writer.Value.Flush();
				}

				Thread.Sleep(100);
			}
		}

		#region Dispose

		public void Dispose()
		{
			_cancellationToken.Cancel();

			_logThread.Wait();
			_logThread.Dispose();
		}

		#endregion
	}
}
