﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CressemExtractLibrary.Data.Odb.Feature;
using CressemExtractLibrary.Data.Odb.Layer;

namespace CressemExtractLibrary.Data.Odb.Step
{
	internal class OdbStepProfile
	{
		private OdbStepProfile() { }

		public OdbStepProfile(OdbFeatures features)
		{
			Features = features;
		}

		public OdbFeatures Features { get; private set; }

		public OdbLayer Layer { get; private set; }
	}
}
