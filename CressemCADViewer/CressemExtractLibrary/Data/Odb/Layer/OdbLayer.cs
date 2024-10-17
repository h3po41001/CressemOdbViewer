using System.Collections.Generic;
using CressemExtractLibrary.Data.Odb.Attribute;
using CressemExtractLibrary.Data.Odb.Feature;
using CressemExtractLibrary.Data.Odb.Matrix;

namespace CressemExtractLibrary.Data.Odb.Layer
{
	internal class OdbLayer
	{		
		public OdbLayer(OdbMatrixLayer matrix, /*OdbAttributeList attrList, */
			OdbFeatures features)
		{
			MatrixLayer = matrix;
			//Attrutes = attrList;
			Features = features;
		}

		public OdbMatrixLayer MatrixLayer { get; private set; }

		//public OdbAttributeList Attrutes { get; private set; }

		public OdbFeatures Features { get; private set; }

		// Components 는 일단 생략. 필요시 구현
	}
}
