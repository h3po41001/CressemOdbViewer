using CressemLogger.Event;
using CressemLogger.Model;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CressemLogger.Factory
{
	internal class LogFactory
	{
		private readonly string _logFolderPath = string.Empty;
		private readonly string _logFileName = string.Empty;

		private readonly Dictionary<string, LogClass> _classLog = new Dictionary<string, LogClass>();

		private LogFactory()
		{
		}

		public LogFactory(string folderPath, string fileName)
		{
			_logFolderPath = folderPath;
			_logFileName = fileName;

			_classLog = new Dictionary<string, LogClass>();
		}

		public bool IsExist(string className)
		{
			return _classLog.ContainsKey(className);
		}

		public bool AddClass(string className, LoggerEvent logFunc)
		{
			if (_classLog.ContainsKey(className))
				return false;

			string folderPath = Path.Combine(_logFolderPath, className);
			Directory.CreateDirectory(folderPath);

			string filePath = Path.Combine(folderPath, _logFileName);

			_classLog.Add(className, new LogClass(className, filePath));
			_classLog[className].LogEvent += logFunc;

			return true;
		}

		public void WriteLog(LogData logData)
		{
			_classLog[logData.ClassName].WriteLog(logData);
		}

		public IEnumerable<LogClass> GetLogClasses()
		{
			return _classLog.Select(x => x.Value);
		}
	}
}
