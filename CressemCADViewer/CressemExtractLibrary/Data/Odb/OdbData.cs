using System.Collections.Generic;
using CressemExtractLibrary.Data.Odb.Feature;
using CressemExtractLibrary.Data.Odb.Font;
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
	}
}
