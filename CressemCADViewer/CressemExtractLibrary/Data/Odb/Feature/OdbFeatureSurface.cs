using System.Collections.Generic;
using CressemExtractLibrary.Data.Interface.Features;
using CressemExtractLibrary.Data.Odb.Symbol;

namespace CressemExtractLibrary.Data.Odb.Feature
{
	internal class OdbFeatureSurface : OdbFeatureBase, IFeatureSurface
	{
		private readonly List<OdbFeaturePolygon> _polygons;

		private OdbFeatureSurface() { }

		public OdbFeatureSurface(int index, bool isMM, string polarity, string decode,
			string attrString) : base(index, isMM, 0, 0, polarity, decode, 0, -1, attrString)
		{
			_polygons = new List<OdbFeaturePolygon>();
		}

		public IEnumerable<IFeaturePolygon> Polygons { get => _polygons; }

		public void AddPolygon(OdbFeaturePolygon polygon)
		{
			_polygons.Add(polygon);
		}
	}
}
