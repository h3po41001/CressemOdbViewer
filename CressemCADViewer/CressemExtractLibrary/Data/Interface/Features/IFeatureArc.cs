namespace CressemExtractLibrary.Data.Interface.Features
{
	public interface IFeatureArc : IFeatureBase
	{
		double Ex { get; }

		double Ey { get; }

		double Cx { get; }

		double Cy { get; }

		bool IsClockWise { get; }
	}
}
