using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CressemCADViewer.Model
{
	public enum ProcessState
	{
		Idle,
		Running,
		Stop,
		Error,
	}

	public enum RotationType
	{		
		CW0,
		CW90,
		CW180,
		CW270,
	}

	public enum FlipType
	{
		None,
		FlipX,
		FlipY,
		FlipXY,
	}
}
