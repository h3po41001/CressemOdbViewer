﻿using CressemExtractLibrary.Data.Interface.Symbol;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolRectangle : OdbSymbolBase, ISymbolRectangle
	{
		protected OdbSymbolRectangle() { }

		public OdbSymbolRectangle(int index, double width, double height) : base(index)
		{
			Width = width;
			Height = height;
		}

		public double Width { get; private set; }

		public double Height { get; private set; }

		public static OdbSymbolRectangle Create(int index, string param)
		{
			string[] split = param.Split('X');
			if (split.Length != 2)
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

			return new OdbSymbolRectangle(index, width, height);
		}
	}
}
