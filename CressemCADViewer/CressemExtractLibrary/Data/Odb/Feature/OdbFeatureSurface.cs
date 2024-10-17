using System.Collections.Generic;
using CressemExtractLibrary.Data.Odb.Attribute;

namespace CressemExtractLibrary.Data.Odb.Feature
{
	internal class OdbFeatureSurface : OdbFeatureBase
	{
		private OdbFeatureSurface()
		{
		}

		public OdbFeatureSurface(bool isMM, string polarity, string decode) : 
			base(isMM, polarity, decode)
		{
		}

		public List<OdbAttributePolygon> Polygons { get; private set; } = new List<OdbAttributePolygon>();

		public void AddPolygon(OdbAttributePolygon polygon)
		{
			Polygons.Add(polygon);
		}
	}
}
