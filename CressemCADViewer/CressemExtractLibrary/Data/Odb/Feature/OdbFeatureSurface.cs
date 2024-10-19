using System.Collections.Generic;
using CressemExtractLibrary.Data.Odb.Symbol;

namespace CressemExtractLibrary.Data.Odb.Feature
{
	internal class OdbFeatureSurface : OdbFeatureBase
	{
		private OdbFeatureSurface()
		{
		}

		public OdbFeatureSurface(int index, bool isMM, string polarity, string decode) : 
			base(index, isMM, polarity, decode)
		{
		}

		public List<OdbSymbolPolygon> Polygons { get; private set; } = new List<OdbSymbolPolygon>();

		public void AddPolygon(OdbSymbolPolygon polygon)
		{
			Polygons.Add(polygon);
		}
	}
}
