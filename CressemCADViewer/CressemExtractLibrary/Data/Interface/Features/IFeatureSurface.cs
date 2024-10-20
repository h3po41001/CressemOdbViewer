using System.Collections.Generic;

namespace CressemExtractLibrary.Data.Interface.Features
{
	public interface IFeatureSurface : IFeatureBase
	{
		IEnumerable<IFeaturePolygon> Polygons { get; }
	}
}
