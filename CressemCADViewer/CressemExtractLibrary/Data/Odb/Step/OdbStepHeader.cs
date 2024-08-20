using System.Drawing;

namespace CressemExtractLibrary.Data.Odb.Step
{
	internal class OdbStepHeader
	{
		public OdbStepHeader(PointF xDatum, PointF yDatum,
			PointF xOrigin, PointF yOrigin,
			int topActive, int bottomActive,
			int rightActive, int leftActive,
			int affectingBom, int affectingBomChanged)
		{
			XDatum = xDatum;
			YDatum = yDatum;
			XOrigin = xOrigin;
			YOrigin = yOrigin;
			TopActive = topActive;
			BottomActive = bottomActive;
			RightActive = rightActive;
			LeftActive = leftActive;
			AffectingBom = affectingBom;
			AffectingBomChanged = affectingBomChanged;
		}

		public PointF XDatum { get; private set; }

		public PointF YDatum { get; private set; }

		public PointF XOrigin { get; private set; }

		public PointF YOrigin { get; private set; }

		public OdbStepRepeat StepRepeat { get; private set; }

		public int TopActive { get; private set; }

		public int BottomActive { get; private set; }

		public int RightActive { get; private set; }

		public int LeftActive { get; private set; }

		public int AffectingBom { get; private set; }

		public int AffectingBomChanged { get; private set; }
	}
}
