using System.Collections.Generic;
using CressemExtractLibrary.Data.Odb.Attribute;
using CressemExtractLibrary.Data.Odb.Layer;
using CressemExtractLibrary.Data.Odb.Matrix;

namespace CressemExtractLibrary.Data.Odb.Step
{
	internal class OdbStep
	{
		public OdbStep(OdbMatrixStep step, 
			OdbStepHeader header, /*OdbAttruteList attrList, */OdbStepProfile profile)
		{
			MatrixStep = step;
			StepHdr = header;
			Layers = new List<OdbLayer>();
			//AttrList = attrList;
			Profile = profile;
		}

		public OdbMatrixStep MatrixStep { get; private set; }

		public OdbStepHeader StepHdr { get; private set; }

		//public OdbAttruteList AttrList { get; private set; }

		public List<OdbLayer> Layers { get; private set; }

		public OdbStepProfile Profile { get; private set; }
	}
}
