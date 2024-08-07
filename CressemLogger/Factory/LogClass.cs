using CressemLogger.Event;
using CressemLogger.Model;
using System.IO;

namespace CressemLogger.Factory
{
	internal class LogClass
	{
		public event LoggerEvent LogEvent;

		private readonly string _name;
		private readonly StreamWriter _logWriter;

		private LogClass() { }

		internal LogClass(string name, string logPath)
		{
			_name = name;
			_logWriter = new StreamWriter(logPath + ".log", true);
		}

		public string Name { get => _name; }

		internal void WriteLog(LogData log)
		{
			_logWriter.WriteLine(log.Log);
			_logWriter.Flush();

			if (log.IsDisplay)
				LogEvent?.Invoke(log.ClassName, log.Log, log.LogColor);
		}
	}
}
