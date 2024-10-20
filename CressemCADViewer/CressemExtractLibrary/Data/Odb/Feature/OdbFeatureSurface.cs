using System.Collections.Generic;
using CressemExtractLibrary.Data.Odb.Symbol;

namespace CressemExtractLibrary.Data.Odb.Feature
{
	public class OdbFeatureSurface : OdbFeatureBase
	{
		private OdbFeatureSurface()
		{
		}

		public OdbFeatureSurface(int index, bool isMM, string polarity, string decode) : 
			base(index, isMM, 0, 0, polarity, decode)
		{
		}

		public List<OdbFeaturePolygon> Polygons { get; private set; } = new List<OdbFeaturePolygon>();

		public void AddPolygon(OdbFeaturePolygon polygon)
		{
			Polygons.Add(polygon);
		}
	}
}
