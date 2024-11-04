using System.Collections.Generic;
using System.Linq;
using CressemExtractLibrary.Data.Interface.Step;

namespace CressemExtractLibrary.Data.Odb.Step
{
	internal class OdbStepHeader : IStepHeader
	{
		private List<OdbStepRepeat> _stepRepeats;

		private OdbStepHeader()
		{
		}

		public OdbStepHeader(double xDatum, double yDatum,
			double xOrigin, double yOrigin,
			List<OdbStepRepeat> stepRepeats,
			double topActive, double bottomActive,
			double rightActive, double leftActive,
			int affectingBom, int affectingBomChanged)
		{
			XDatum = xDatum;
			YDatum = yDatum;
			XOrigin = xOrigin;
			YOrigin = yOrigin;
			_stepRepeats = new List<OdbStepRepeat>(stepRepeats);
			TopActive = topActive;
			BottomActive = bottomActive;
			RightActive = rightActive;
			LeftActive = leftActive;
			AffectingBom = affectingBom;
			AffectingBomChanged = affectingBomChanged;
		}

		public double XDatum { get; private set; }

		public double YDatum { get; private set; }

		public double XOrigin { get; private set; }

		public double YOrigin { get; private set; }

		public IEnumerable<IRepeatInfo> StepRepeats { get => _stepRepeats.Cast<IRepeatInfo>(); }

		public double TopActive { get; private set; }

		public double BottomActive { get; private set; }

		public double RightActive { get; private set; }

		public double LeftActive { get; private set; }

		public int AffectingBom { get; private set; }

		public int AffectingBomChanged { get; private set; }
	}
}
