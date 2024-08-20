using System.Drawing;

namespace CressemLogger.Event
{
	public delegate void LoggerEvent(string className, string message, Color color);
}
