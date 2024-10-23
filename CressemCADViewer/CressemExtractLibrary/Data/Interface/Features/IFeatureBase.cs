using CressemExtractLibrary.Data.Interface.Symbol;

namespace CressemExtractLibrary.Data.Interface.Features
{
	public interface IFeatureBase
	{
		bool IsMM { get; }

		double X { get; }

		double Y { get; }

		string Polarity { get; }

		// 0 : 0도, 1 : 90도, 2 : 180도, 3 : 270도, 4 : 0도 X축 미러, 5 : 90도 X축 미러, 6 : 180도 X축 미러, 7 : 270도 X축 미러
		// 8 :  any angle rotation, no mirror, 9 : any angle rotation, X-axis mirror
		int OrientDef { get; }

		ISymbolBase FeatureSymbol { get; }
	}
}
