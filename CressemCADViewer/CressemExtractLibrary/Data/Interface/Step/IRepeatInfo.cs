namespace CressemExtractLibrary.Data.Interface.Step
{
	public interface IRepeatInfo
	{
		string Name { get; }

		double Sx { get; }

		double Sy { get; }

		double Dx { get; }

		double Dy { get; }

		int Nx { get; }

		int Ny { get; }

		double Angle { get; }

		bool IsFlipHorizontal { get; }
	}
}
