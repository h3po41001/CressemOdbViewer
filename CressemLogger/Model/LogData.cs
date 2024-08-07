using System;
using System.Drawing;

namespace CressemLogger.Model
{
	public class LogData
	{
		private readonly LogType _logType;
		private readonly Color _color;
		private readonly string _className = string.Empty;
		private readonly string _log = string.Empty;
		private readonly bool _isDisplay = true;

		private LogData()
		{
		}

		public LogData(LogType logType, string className, string log, Color color, bool isDisplay)
		{
			_logType = logType;
			_className = className;

			string logTypeString = $"[{_logType}]";
			string classNameString = $"[{_className}]";

			_log = $"[{DateTime.Now:HH:mm:ss.fff}]" +
				$"{logTypeString.ToUpper(),-7}" +
				$"{classNameString.ToUpper(),-10}" + log;

			_color = color;
			_isDisplay = isDisplay;
		}

		public LogType LogType { get => _logType; }

		public string ClassName { get => _className; }

		public string Log { get => _log; }

		public Color LogColor { get => _color; }

		public bool IsDisplay { get => _isDisplay; }
	}

}
