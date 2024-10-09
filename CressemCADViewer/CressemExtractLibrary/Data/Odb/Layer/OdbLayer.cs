using System.Collections.Generic;
using CressemExtractLibrary.Data.Odb.Attribute;
using CressemExtractLibrary.Data.Odb.Feature;
using CressemExtractLibrary.Data.Odb.Matrix;

namespace CressemExtractLibrary.Data.Odb.Layer
{
	internal class OdbLayer
	{		
		public OdbLayer(OdbMatrixLayer layer, /*OdbAttruteList attrList,*/
			List<OdbFeatures> features)
		{
			MatrixLayer = layer;
			//AttruteList = attrList;
			FeaturesList = new List<OdbFeatures>(features);
		}

		public OdbMatrixLayer MatrixLayer { get; private set; }

		//public OdbAttruteList AttruteList { get; private set; }

		public List<OdbFeatures> FeaturesList { get; private set; }

		// Components 는 일단 생략. 필요시 구현
	}
}
