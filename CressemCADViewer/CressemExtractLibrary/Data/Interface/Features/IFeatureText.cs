using CressemExtractLibrary.Data.Interface.Font;

namespace CressemExtractLibrary.Data.Interface.Features
{
	public interface IFeatureText : IFeatureBase
	{
		string Font { get; }

		double SizeX { get; }

		double SizeY { get; }

		double WidthFactor { get; }

		string Text { get; }

		IFont FeatureFont { get; }
	}
}
