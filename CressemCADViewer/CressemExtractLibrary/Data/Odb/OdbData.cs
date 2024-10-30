using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CressemExtractLibrary.Data.Interface.Features;
using CressemExtractLibrary.Data.Odb.Feature;
using CressemExtractLibrary.Data.Odb.Font;
using CressemExtractLibrary.Data.Odb.Layer;
using CressemExtractLibrary.Data.Odb.Matrix;
using CressemExtractLibrary.Data.Odb.Step;
using CressemExtractLibrary.Data.Odb.Symbol;

namespace CressemExtractLibrary.Data.Odb
{
	internal class OdbData : ExtractData
	{
		private OdbData() : base() { }

		public OdbData(string loadPath, string savePath) : base(loadPath, savePath)
		{
		}

		public OdbMatrixInfo OdbMatrixInfo { get; set; }

		public List<OdbFont> OdbFonts { get; set; } = new List<OdbFont>();

		public List<OdbSymbolUser> OdbUserSymbols { get; set; } = new List<OdbSymbolUser>();

		public List<OdbStep> OdbSteps { get;  set; } = new List<OdbStep>();

		public override string[] GetStepNames()
		{
			List<string> stepNames = new List<string>();
			foreach (OdbStep step in OdbSteps)
			{
				stepNames.Add(step.MatrixStep.Name);
			}

			return stepNames.ToArray();
		}

		public override string[] GetLayerNames(string stepName)
		{
			foreach (OdbStep step in OdbSteps)
			{
				if (step.MatrixStep.Name == stepName)
				{
					List<string> layerNames = new List<string>();
					layerNames.AddRange(step.Layers.Select(x => x.MatrixLayer.Name));

					return layerNames.ToArray();
				}
			}

			return null;
		}

		public override IFeatureBase GetStepRoi(string stepName)
		{
			foreach (OdbStep step in OdbSteps)
			{
				if (step.MatrixStep.Name.Equals(stepName) is true)
				{					
					foreach (var feature in step.Profile.Features.FeatureList)
					{
						if (feature is OdbFeatureSurface surfaces)
						{
							return surfaces;
						}
					}
				}
			}

			return null;
		}

		public override IFeatureBase[] GetFeatures(string stepName, string layerName,
			out double xDatum, out double yDatum)
		{
			xDatum = 0.0;
			yDatum = 0.0;

			foreach (OdbStep step in OdbSteps)
			{
				if (step.MatrixStep.Name.Equals(stepName) is true)
				{					
					foreach (OdbLayer layer in step.Layers)
					{
						if (layer.MatrixLayer.Name.Equals(layerName) is true)
						{
							xDatum = step.StepHdr.XDatum;
							yDatum = step.StepHdr.YDatum;

							var line = layer.Features.FeatureList.Where(x => x is IFeatureLine);

							return layer.Features.FeatureList.ToArray();
						}
					}
				}
			}

			return null;
		}
	}
}
