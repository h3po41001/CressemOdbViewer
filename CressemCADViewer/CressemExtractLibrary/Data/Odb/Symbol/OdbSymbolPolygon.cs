using System.Collections.Generic;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolPolygon
	{
		private OdbSymbolPolygon() { }

		public OdbSymbolPolygon(List<OdbPolygonAttr> attributes)
		{
			Attributes = new List<OdbPolygonAttr>(attributes);
		}

		public List<OdbPolygonAttr> Attributes { get; private set; }
	}

	internal class OdbPolygonAttr
	{
		protected OdbPolygonAttr() { }

		public OdbPolygonAttr(double x, double y)
		{
			X = x;
			Y = y;
		}

		public double X { get; private set; }

		public double Y { get; private set; }
	}

	/// <summary>
	/// OB : Open Boundary, Polygon Start
	/// </summary>
	internal class OdbPolygonOB : OdbPolygonAttr
	{
		private OdbPolygonOB() : base() { }

		public OdbPolygonOB(double sbx, double sby, 
			string polygonType) : base(sbx, sby)
		{
			PolygonType = polygonType;
		}

		// I : Island, H : Hole
		public string PolygonType { get; private set; }
	}

	/// <summary>
	/// OS : Open Segment
	/// </summary>
	internal class OdbPolygonOS : OdbPolygonAttr
	{
		private OdbPolygonOS() : base() { }

		public OdbPolygonOS(double x, double y) : base(x, y)
		{
		}
	}

	/// <summary>
	/// OC : Open Curve
	/// </summary>
	internal class OdbPolygonOC : OdbPolygonAttr
	{
		private OdbPolygonOC() : base() { }

		public OdbPolygonOC(double ex, double ey,
			double cx, double cy, bool isClockWise) : base(ex, ey)
		{
			CurveX = cx;
			CurveY = cy;
			IsClockWise = isClockWise;
		}

		public double CurveX { get; private set; }

		public double CurveY { get; private set; }

		public bool IsClockWise { get; private set; }
	}
}
