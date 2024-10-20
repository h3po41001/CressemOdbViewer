using System.Collections.Generic;

namespace CressemExtractLibrary.Data.Interface.Features
{
	public interface IFeaturePolygon : IFeatureBase
	{
		string PolygonType { get; }

		IEnumerable<IFeatureBase> Features { get; }
	}
}
