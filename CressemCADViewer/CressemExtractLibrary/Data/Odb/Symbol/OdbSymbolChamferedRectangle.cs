﻿using System.Linq;
using CressemExtractLibrary.Data.Interface.Symbol;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolChamferedRectangle : OdbSymbolEditedCorner, ISymbolChamferedRectangle
	{
		private OdbSymbolChamferedRectangle() { }

		public OdbSymbolChamferedRectangle(int index, 
			double width, double height,
			double cornerRadius, string corners) : base(index, cornerRadius, corners, 4)
		{
			Width = width;
			Height = height;
		}

		public double Width { get; private set; }

		public double Height { get; private set; }

		// RT = 0, LT = 1, LB = 2, RB = 3
		public bool[] IsChamfered { get => IsEditedCorner; }

		public static OdbSymbolChamferedRectangle Create(int index, string param)
		{
			string[] split = param.Split('X');
			if (split.Length != 4)
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

			if (double.TryParse(string.Concat(split[2].Skip(1)), 
				out double cornerRadius) is false)
			{
				return null;
			}

			return new OdbSymbolChamferedRectangle(index,
				width, height, cornerRadius, split[3]);
		}
	}
}
