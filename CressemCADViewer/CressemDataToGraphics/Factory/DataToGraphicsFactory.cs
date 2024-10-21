using System;
using CressemDataToGraphics.Model.Graphics.Shape;
using CressemExtractLibrary.Data.Interface.Features;
using ImageControl.Shape.Interface;

namespace CressemDataToGraphics.Factory
{
	internal class DataToGraphicsFactory
	{
		private static DataToGraphicsFactory _instance;

		private DataToGraphicsFactory() { }

		public static DataToGraphicsFactory Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new DataToGraphicsFactory();
				}

				return _instance;
			}
		}

		public IShapeBase DataToGdiPlus(bool useMM, 
			float pixelResolution, IFeatureBase feature)
		{
			if (feature is null)
			{
				return null;
			}

			if (feature is IFeatureArc arc)
			{
				return ShapeArc.CreateGdiPlus(useMM, pixelResolution, arc);
			}
			else if (feature is IFeatureBarcode barcode)
			{
				//return ShapeBarcode.CreateGdiPlus(pixelResolution, barcode);
			}
			else if (feature is IFeatureLine line)
			{
				return ShapeLine.CreateGdiPlus(useMM, pixelResolution, line);
			}
			else if (feature is IFeaturePolygon polygon)
			{
				return ShapePolygon.CreateGdiPlus(useMM, pixelResolution, true, polygon);
			}
            else if (feature is IFeatureSurface surface)
            {
                 return ShapeSurface.CreateGdiPlus(useMM, pixelResolution, surface);
			}
			else if (feature is IFeatureText text)
			{
				//return ShapeText.CreateGdiPlus(pixelResolution, text);
			}

			return null;// throw new NotImplementedException("Not Implemented Shape [Gdi Plus]");
		}

		public IShapeBase DataToOpenGl(float pixelResolution, IFeatureBase feature)
		{
			if (feature is null)
			{
				return null;
			}

			if (feature is IFeatureArc featureArc)
			{
				//return ShapeArc.CreateOpenGl(pixelResolution, featureArc);
			}
			else if (feature is IFeatureBarcode featureBarcode)
			{
				//return ShapeBarcode.CreateOpenGl(pixelResolution, featureBarcode);
			}
			else if (feature is IFeatureLine featureLine)
			{
				//return ShapeLine.CreateOpenGl(pixelResolution, featureLine);
			}
			else if (feature is IFeaturePolygon featurePolygon)
			{
				//return ShapePolygon.CreateOpenGl();
			}
			else if (feature is IFeatureSurface featureSurface)
			{
				//return ShapeSurface.CreateOpenGl();
			}
			else if (feature is IFeatureText featureText)
			{
				//return ShapeText.CreateOpenGl();
			}

			throw new NotImplementedException("Not Implemented Shape [Open Gl]");
		}
	}
}
