using System.Drawing;
using System.Linq;

namespace CressemExtractLibrary.Data.Odb.Symbol
{
	internal class OdbSymbolEditedCorner : OdbSymbolBase
	{
		protected OdbSymbolEditedCorner() { }

		public OdbSymbolEditedCorner(PointF pos, double cornerRadius, 
			string corners, int cornerNum) : base(pos)
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
				int index = int.Parse(corner);
				isEdited[index] = true;
			}

			return isEdited;
		}
	}
}
