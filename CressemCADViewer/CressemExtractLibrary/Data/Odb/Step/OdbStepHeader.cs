using System.Collections.Generic;
using System.Drawing;

namespace CressemExtractLibrary.Data.Odb.Step
{
	internal class OdbStepHeader
	{
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
			StepRepeats = stepRepeats;
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

		public List<OdbStepRepeat> StepRepeats { get; private set; }

		public double TopActive { get; private set; }

		public double BottomActive { get; private set; }

		public double RightActive { get; private set; }

		public double LeftActive { get; private set; }

		public int AffectingBom { get; private set; }

		public int AffectingBomChanged { get; private set; }
	}
}
