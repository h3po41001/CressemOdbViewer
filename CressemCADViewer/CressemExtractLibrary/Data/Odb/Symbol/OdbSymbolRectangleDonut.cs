﻿using CressemExtractLibrary.Data.Interface.Symbol;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolRectangleDonut : OdbSymbolBase, ISymbolRectangleDonut
	{
		protected OdbSymbolRectangleDonut()
		{
		}

		public OdbSymbolRectangleDonut(int index, double width, double height, 
			double lineWidth) : base(index)
		{
			Width = width;
			Height = height;
			LineWidth = lineWidth;
		}

		public double Width { get; private set; }

		public double Height { get; private set; }

		public double LineWidth { get; private set; }

		public static OdbSymbolRectangleDonut Create(int index, string param)
		{
			string[] split = param.Split('X');
			if (split.Length != 3)
			{
				return null;
			}

			if (double.TryParse(split[0], out double width) is false)
			{
				return null;
			}

			if (double.TryParse(split[1], out double height) is false)
			{
				return null;
			}

			if (double.TryParse(split[2], out double lineWidth) is false)
			{
				return null;
			}

			return new OdbSymbolRectangleDonut(index, width, height, lineWidth);
		}
	}
}
