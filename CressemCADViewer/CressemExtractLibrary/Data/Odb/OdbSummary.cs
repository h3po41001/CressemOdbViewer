using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CressemExtractLibrary.Data.Odb
{
	internal class OdbSummary
	{
		private OdbSummary() { }

		public OdbSummary(int size, int sum, 
			string date, string time, string version, string user)
		{
			Size = size;
			Sum = sum;
			Date = date;
			Time = time;
			Version = version;
			User = user;
		}

		public int Size { get; private set; }

		public int Sum { get; private set; }

		public string Date { get; private set; }

		public string Time { get; private set; }

		public string Version { get; private set; }

		public string User { get; private set; }
	}
}
