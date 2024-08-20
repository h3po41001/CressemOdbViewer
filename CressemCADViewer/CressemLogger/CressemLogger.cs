using CressemLogger.Event;
using CressemLogger.Factory;

namespace CressemLogger
{
	public class CLogger
	{
		private static CLogger _logger;
		private readonly LogManager _manager = new LogManager();

		private CLogger() { }

		public static CLogger Instance
		{
			get
			{
				if (_logger is null)
					_logger = new CLogger();

				return _logger;
			}
		}

		public void Initialize(string folderPath)
		{
			_manager.Initialize(folderPath);
			_manager.LogStart();
		}

		public bool AddLogClass(string className, LoggerEvent logEvent)
		{
			return _manager.AddClass(className, logEvent);
		}

		public bool AddInfoLog(string className, string message, bool display = false)
		{
			return _manager.AddInfoLog(className, message, display);
		}

		public bool AddWarningLog(string className, string message, bool display = true)
		{
			return _manager.AddWarningLog(className, message, display);
		}

		public bool AddErrorLog(string className, string message, bool display = true)
		{
			return _manager.AddErrorLog(className, message, display);
		}

		public bool AddDebugLog(string className, string message, bool display = false)
		{
			return _manager.AddDebugLog(className, message, display);
		}

		public bool AddPointLog(string className, string message, bool display = true)
		{
			return _manager.AddPointLog(className, message, display);
		}

		public bool AddLogBroadcast(string message, bool display = false)
		{
			return _manager.AddLogBroadcast(message, display);
		}

		#region Dispose

		public void Dispose()
		{
			_manager.Dispose();
		}

		#endregion
	}
}
