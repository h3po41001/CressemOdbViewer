using System.Linq;
using CressemExtractLibrary.Data.Interface.Symbol;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolEditedCorner : OdbSymbolBase, ISymbolEditedCorner
	{
		protected OdbSymbolEditedCorner() { }

		public OdbSymbolEditedCorner(int index, double cornerRadius,
			string corners, int cornerNum) : base(index)
		{
			CornerRadius = cornerRadius;
			IsEditedCorner = ConvertCornerFlag(corners, cornerNum);
		}

		public double CornerRadius { get; private set; }

		public bool[] IsEditedCorner { get; private set; } = null;

		public bool[] ConvertCornerFlag(string corners, int cornerNum)
		{
			bool[] isEdited = new bool[cornerNum];

			var flags = corners.ToCharArray()?.Select(x => x.ToString());
			foreach (var corner in flags)
			{
				if (int.TryParse(corner, out int index) is false)
				{
					continue;
				}

				isEdited[index] = true;
			}

			return isEdited;
		}
	}
}
