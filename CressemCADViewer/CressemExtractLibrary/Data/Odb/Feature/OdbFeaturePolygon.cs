using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CressemExtractLibrary.Data.Odb.Feature
{
	internal class OdbFeaturePolygon : OdbFeatures
	{
		private OdbFeaturePolygon() 
		{
		}

		public OdbFeaturePolygon(List<Tuple<double, double>> points)
		{
			Points = points;
		}

		public List<Tuple<double, double>> Points { get; private set; }
	}
}
