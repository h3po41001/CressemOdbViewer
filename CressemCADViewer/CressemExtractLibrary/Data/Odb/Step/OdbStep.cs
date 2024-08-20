using System.Collections.Generic;
using CressemExtractLibrary.Data.Odb.Attribute;
using CressemExtractLibrary.Data.Odb.Layer;
using CressemExtractLibrary.Data.Odb.Matrix;

namespace CressemExtractLibrary.Data.Odb.Step
{
	internal class OdbStep
	{
		public OdbStep(OdbMatrixStep step, OdbStepHeader header, OdbAttruteList attrList)
		{
			MatrixStep = step;
			StepHdr = header;
			AttrList = attrList;
			Layers = new List<OdbLayer>();
		}
		public OdbMatrixStep MatrixStep { get; private set; }

		public OdbStepHeader StepHdr { get; private set; }

		public OdbAttruteList AttrList { get; private set; }

		//public List<OdbStepNetList> CadNetList { get; private set; }

		//public List<OdbStepNetList> RefNetList { get; private set; }

		//public List<OdbStepNetList> CurNetList { get; private set; }

		public List<OdbLayer> Layers { get; private set; }

		public OdbStepProfile Profile { get; private set; }
	}
}
