using System.Collections.Generic;
using CressemExtractLibrary.Data.Odb.Feature;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	public class OdbFeaturePolygon
	{
		public OdbFeaturePolygon(string polygonType)
		{
			PolygonType = polygonType;
			Features = new List<OdbFeatureBase>();
		}

		public string PolygonType { get; private set; }

		public List<OdbFeatureBase> Features { get; private set; }
	}
}
