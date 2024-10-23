using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CressemExtractLibrary.Data.Interface.Features
{
	public interface IFeatureText : IFeatureBase
	{
		string Font { get; }

		double SizeX { get; }

		double SizeY { get; }

		double WidthFactor { get; }

		string Text { get; }
	}
}
