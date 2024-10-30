using System.Collections.Generic;
using CressemExtractLibrary.Data.Interface.Features;

namespace CressemExtractLibrary.Data.Odb.Feature
{
	internal class OdbFeatureSurfaces : OdbFeatureBase, IFeatureSurfaces
	{
		private List<OdbFeatureSurface> _features;

		private OdbFeatureSurfaces() { }

		public OdbFeatureSurfaces(IEnumerable<OdbFeatureSurface> surfaces)
		{
			_features = new List<OdbFeatureSurface>(surfaces);
		}

		public IEnumerable<IFeatureSurface> Features { get => _features; }
	}
}
