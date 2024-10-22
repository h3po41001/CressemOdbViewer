using CressemExtractLibrary.Data.Interface.Features;

namespace CressemExtractLibrary.Data.Odb.Feature
{
	internal class OdbFeatureArc : OdbFeatureBase, IFeatureArc
	{
		private OdbFeatureArc() { }

		public OdbFeatureArc(int index, bool isMM, double sx, double sy,
			double ex, double ey,
			double cx, double cy, int symbolNum, 
			string polarity, string decode, string cw, 
			string attrString) : base(index, isMM, sx, sy, polarity, decode, symbolNum, attrString)
		{
			Ex = ex;
			Ey = ey;
			Cx = cx;
			Cy = cy;
			// Y : CW, N : CCW
			IsClockWise = cw.Equals("Y") is true;
		}

		public double Ex { get; private set; }

		public double Ey { get; private set; }

		public double Cx { get; private set; }

		public double Cy { get; private set; }

		public bool IsClockWise { get; private set; }

		public static OdbFeatureArc Create(int index, bool isMM, string[] param)
		{
			if (param.Length != 11)
			{
				return null;
			}

			if (param[0] != "A")
			{
				return null;
			}

			if (double.TryParse(param[1], out double sx) is false)
			{
				return null;
			}

			if (double.TryParse(param[2], out double sy) is false)
			{
				return null;
			}

			if (double.TryParse(param[3], out double ex) is false)
			{
				return null;
			}

			if (double.TryParse(param[4], out double ey) is false)
			{
				return null;
			}

			if (double.TryParse(param[5], out double cx) is false)
			{
				return null;
			}

			if (double.TryParse(param[6], out double cy) is false)
			{
				return null;
			}

			if (int.TryParse(param[7], out int symbolNum) is false)
			{
				return null;
			}

			string[] splited = param[10].Split(';');

			string cw = splited[0];
			string attrString = splited.Length > 1 ? splited[1] : string.Empty;

			return new OdbFeatureArc(index, isMM, sx, sy, ex, ey, cx, cy, 
				symbolNum, param[8], param[9], cw, attrString);
		}
	}
}
