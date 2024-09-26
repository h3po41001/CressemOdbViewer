using System.Collections.Generic;

namespace CressemExtractLibrary.Data.Odb.Matrix
{
	internal class OdbMatrixInfo
	{
		private OdbMatrixInfo() 
		{
		}

		public OdbMatrixInfo(OdbSummary summary, List<OdbMatrixStep> steps, List<OdbMatrixLayer> layers)
		{
			Summary = summary;
			Steps = steps;
			Layers = layers;
		}

		public OdbSummary Summary { get; private set; }

		public List<OdbMatrixStep> Steps { get; private set; }

		public List<OdbMatrixLayer> Layers { get; private set; }
	}
}
