﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CressemExtractLibrary.Data.Odb.Feature
{
	internal class OdbFeatureArc : OdbFeatureBase
	{
		private OdbFeatureArc() { }

		public OdbFeatureArc(bool isMM, double sx, double sy,
			double ex, double ey, double cx, double cy,
			int symbolNum, string polarity, string decode, string cw) : base(isMM, polarity, decode)
		{
			Sx = sx;
			Sy = sy;
			Ex = ex;
			Ey = ey;
			Cx = cx;
			Cy = cy;
			SymbolNum = symbolNum;
			Cw = cw;
		}

		public double Sx { get; private set; }

		public double Sy { get; private set; }

		public double Ex { get; private set; }

		public double Ey { get; private set; }

		public double Cx { get; private set; }

		public double Cy { get; private set; }

		public int SymbolNum { get; private set; }

		// Y : CW, N : CCW
		public string Cw { get; private set; }

		public static OdbFeatureArc Create(bool isMM, string[] param)
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

			return new OdbFeatureArc(isMM, sx, sy, ex, ey, cx, cy, 
				symbolNum, param[8], param[9], param[10]);
		}
	}
}