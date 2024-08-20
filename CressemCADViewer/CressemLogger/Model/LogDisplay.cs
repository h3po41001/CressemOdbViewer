using System.Drawing;

namespace CressemLogger.Model
{
	public class LogDisplay
	{
		public LogDisplay(string name, string log, Color color)
		{
			Name = name;
			Log = log;
			Color = color.ToKnownColor().ToString();
		}

		public string Name { get; private set; } = string.Empty;

		public string Log { get; private set; } = string.Empty;

		public string Color { get; private set; } = string.Empty;
	}
}
