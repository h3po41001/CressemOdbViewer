using System.Collections.Generic;
using CressemExtractLibrary.Data.Interface.Features;
using CressemExtractLibrary.Data.Odb.Feature;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbFeaturePolygon : OdbFeatureBase, IFeaturePolygon
	{
		private readonly List<OdbFeatureBase> _features;

		public OdbFeaturePolygon(string polygonType)
		{
			PolygonType = polygonType;
			_features = new List<OdbFeatureBase>();
		}

		public string PolygonType { get; private set; }

		public IEnumerable<IFeatureBase> Features { get => _features; }

		public void AddFeature(OdbFeatureBase feature)
		{
			_features.Add(feature);
		}
	}
}
