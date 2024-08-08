using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CressemUtil.Zip
{
	public enum ErrorType
	{
		None,
		NotExistsFile,
		NotExistsDirectory,
		NotSupportExtension,
		NotSupportArchive,
	}
}
