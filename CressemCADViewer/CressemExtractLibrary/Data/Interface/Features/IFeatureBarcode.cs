namespace CressemExtractLibrary.Data.Interface.Features
{
	public interface IFeatureBarcode : IFeatureBase
	{
		string Font { get; }

		double ElementWidth { get; }

		double BarcodeHeight { get; }
	}
}
