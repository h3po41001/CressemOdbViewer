using System.Collections.Generic;

namespace CressemExtractLibrary.Data.Odb.Matrix
{
	internal class OdbMatrixInfo
	{
		public OdbMatrixInfo() { }

		public List<OdbMatrixStep> Steps { get; set; } = new List<OdbMatrixStep>();

		public List<OdbMatrixLayer> Layers { get; set; } = new List<OdbMatrixLayer>();
	}
}
