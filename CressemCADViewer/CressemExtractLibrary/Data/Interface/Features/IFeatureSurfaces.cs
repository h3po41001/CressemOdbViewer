using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CressemExtractLibrary.Data.Interface.Features
{
	public interface IFeatureSurfaces : IFeatureBase
	{
		IEnumerable<IFeatureSurface> Features { get; }
	}
}
