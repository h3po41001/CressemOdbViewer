using System.Collections.Generic;
using CressemExtractLibrary.Data.Odb.Layer;
using CressemExtractLibrary.Data.Odb.Matrix;

namespace CressemExtractLibrary.Data.Odb.Step
{
	internal class OdbStep
	{
		public OdbStep(OdbMatrixStep step, 
			OdbStepHeader header, OdbStepProfile profile,/*OdbAttruteList attrList, */
			List<OdbLayer> layers)
		{
			MatrixStep = step;
			StepHdr = header;
			Profile = profile;
			//AttrList = attrList;
			Layers = new List<OdbLayer>(layers);
		}

		public OdbMatrixStep MatrixStep { get; private set; }

		public OdbStepHeader StepHdr { get; private set; }

		public OdbStepProfile Profile { get; private set; }

		//public OdbAttruteList AttrList { get; private set; }
		public List<OdbLayer> Layers { get; private set; }
	}
}
