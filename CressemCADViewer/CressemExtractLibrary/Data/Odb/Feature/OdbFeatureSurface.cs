using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CressemExtractLibrary.Data.Odb.Feature
{
	internal class OdbFeatureSurface : OdbFeatures
	{
		private OdbFeatureSurface()
		{
		}

		public OdbFeatureSurface(string polarity, string decode)
		{
			Polarity = polarity;
			Decode = decode;
		}

		public string Polarity { get; private set; }

		// gerber decode
		public string Decode { get; private set; }

		public List<OdbFeaturePolygon> Polygons { get; private set; } = new List<OdbFeaturePolygon>();

		public void AddPolygon(OdbFeaturePolygon polygon)
		{
			Polygons.Add(polygon);
		}
	}
}
