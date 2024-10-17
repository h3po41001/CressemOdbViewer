using System.Collections.Generic;

namespace CressemExtractLibrary.Data.Odb.Attribute
{
	internal class OdbAttributePolygon
	{
		private OdbAttributePolygon()
		{
		}

		public OdbAttributePolygon(bool isMM, List<OdbPolygonAttr> attributes)
		{
			IsMM = isMM;
			Attributes = new List<OdbPolygonAttr>(attributes);
		}

		public bool IsMM { get; private set; }

		public List<OdbPolygonAttr> Attributes { get; private set; }
	}

	internal class OdbPolygonAttr
	{
		protected OdbPolygonAttr() { }

		public OdbPolygonAttr(bool isMM)
		{
			IsMM = isMM;
		}

		public bool IsMM { get; private set; }
	}

	/// <summary>
	/// OB : Open Boundary, Polygon Start
	/// </summary>
	internal class OdbPolygonOB : OdbPolygonAttr
	{
		private OdbPolygonOB() : base() { }

		public OdbPolygonOB(double xbs, double ybs, string polygonType,
			bool isMM) : base(isMM)
		{
			XBS = xbs;
			YBS = ybs;
			PolygonType = polygonType;
		}

		public double XBS { get; private set; }

		public double YBS { get; private set; }

		// I : Island, H : Hole
		public string PolygonType { get; private set; }
	}

	/// <summary>
	/// OS : Open Segment
	/// </summary>
	internal class OdbPolygonOS : OdbPolygonAttr
	{
		private OdbPolygonOS() : base() { }

		public OdbPolygonOS(double x, double y, bool isMM) : base(isMM)
		{
			X = x;
			Y = y;
		}

		public double X { get; private set; }

		public double Y { get; private set; }
	}

	/// <summary>
	/// OC : Open Curve
	/// </summary>
	internal class OdbPolygonOC : OdbPolygonAttr
	{
		private OdbPolygonOC() : base() { }

		public OdbPolygonOC(double ex, double ey,
			double cx, double cy, bool isClockWise, bool isMM) : base(isMM)
		{
			EndX = ex;
			EndY = ey;
			CurveX = cx;
			CurveY = cy;
			IsClockWise = isClockWise;
		}

		public double EndX { get; private set; }

		public double EndY { get; private set; }

		public double CurveX { get; private set; }

		public double CurveY { get; private set; }

		public bool IsClockWise { get; private set; }
	}
}
