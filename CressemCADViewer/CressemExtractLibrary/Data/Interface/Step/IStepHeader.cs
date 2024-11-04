using System.Collections.Generic;

namespace CressemExtractLibrary.Data.Interface.Step
{
	public interface IStepHeader
	{
		double XDatum { get; }

		double YDatum { get; }

		double XOrigin { get; }

		double YOrigin { get; }

		IEnumerable<IRepeatInfo> StepRepeats { get; }

		double TopActive { get; }

		double BottomActive { get; }

		double LeftActive { get; }

		double RightActive { get; }
	}
}
