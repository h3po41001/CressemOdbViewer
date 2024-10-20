using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CressemExtractLibrary.Convert;
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

		public override RectangleF GetStepRoi(string stepName)
		{
			RectangleF stepRoi = new RectangleF();

			foreach (OdbStep step in OdbSteps)
			{
				if (step.MatrixStep.Name.Equals(stepName) is true)
				{
					double left = double.MaxValue;
					double right = double.MinValue;
					double top = double.MinValue;
					double bottom = double.MaxValue;
					
					bool isMM = false;
					foreach (var feature in step.Profile.Features.FeatureList)
					{
						if (feature is OdbFeatureSurface surface)
						{
							foreach (var polygon in surface.Polygons)
							{
								foreach (var polyFeature in polygon.Features)
								{
										left = polyFeature.X < left ? polyFeature.X : left;
										right = polyFeature.X > right ? polyFeature.X : right;
										top = polyFeature.Y > top ? polyFeature.Y : top;
										bottom = polyFeature.Y < bottom ? polyFeature.Y : bottom;
								}
							}

							isMM = feature.IsMM;
							break;
						}
					}

					stepRoi = new RectangleF((float)left, (float)top, (float)(right - left), (float)(top - bottom));
					if (isMM is false)
					{
						stepRoi = Converter.Instance.ConvertInchToMM(stepRoi);
					}
					break;
				}
			}

			return stepRoi;
		}

		public override OdbFeatureBase[] GetFeatures(string stepName, string layerName)
		{
			foreach (OdbStep step in OdbSteps)
			{
				if (step.MatrixStep.Name.Equals(stepName) is true)
				{					
					foreach (OdbLayer layer in step.Layers)
					{
						if (layer.MatrixLayer.Name.Equals(layerName) is true)
						{
							return layer.Features.FeatureList.ToArray();
						}
					}
				}
			}

			return null;
		}
	}
}
