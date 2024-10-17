using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CressemExtractLibrary.Data.Odb.Feature;
using CressemExtractLibrary.Data.Odb.Symbol;

namespace CressemExtractLibrary.Data.Odb.Loader
{
	internal class OdbSymbolLoader : OdbLoader
	{
		private static OdbSymbolLoader _instance;

		private OdbSymbolLoader() { }

		public static OdbSymbolLoader Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new OdbSymbolLoader();
				}

				return _instance;
			}
		}

		public bool LoadStandardSymbols(string symbolData, out OdbSymbolBase odbFeatureSymbol)
		{
			odbFeatureSymbol = null;
			if (symbolData.Length == 0)
			{
				return false;
			}

			string pattern = @"[-+.\d]";
			Match match = Regex.Match(symbolData, pattern);

			string name = string.Concat(symbolData.Take(match.Index));
			if (name == string.Empty)
			{
				return false;
			}

			string param = string.Concat(symbolData.Skip(match.Index));
			if (param == string.Empty)
			{
				return false;
			}

			odbFeatureSymbol = MakeSymbol(name, param);

			return (odbFeatureSymbol is null) is false;
		}

		public bool LoadUserSymbols(string dirPath, 
			out List<OdbSymbolUser> userSymbols)
		{
			userSymbols = null;

			string symbolsPath = Path.Combine(dirPath, SymbolsFolderName);
			if (Directory.Exists(symbolsPath) is false)
			{
				return false;
			}

			string[] symbolDirPaths = Directory.GetDirectories(symbolsPath);
			if (symbolsPath.Length == 0)
			{
				return false;
			}

			ConcurrentQueue<OdbSymbolUser> symbolQueue = new ConcurrentQueue<OdbSymbolUser>();

			Parallel.ForEach(symbolDirPaths, symbolDirPath =>
			{
				string symbolFeaturesPath = Path.Combine(symbolDirPath, FeaturesFileName);

				if (OdbFeaturesLoader.Instance.Load(symbolFeaturesPath,
					null,
					out OdbFeatures features) is false)
				{
					return;
				}

				string symbolName = Path.GetFileName(symbolDirPath);
				symbolQueue.Enqueue(new OdbSymbolUser(symbolName, symbolDirPath, features));
			});

			userSymbols = new List<OdbSymbolUser>(symbolQueue);
			return true;
		}

		private OdbSymbolBase MakeSymbol(string name, string param)
		{
			if (name.ToUpper().Equals("R") is true)
			{
				// Round
				return OdbSymbolRound.Create(param);
			}
			else if (name.ToUpper().Equals("S") is true)
			{
				// Square
				return OdbSymbolSquare.Create(param);
			}
			else if (name.ToUpper().Equals("RECT") is true)
			{
				if (param.ToUpper().Contains("XR") is true)
				{
					// Rounded Rectangle
					return OdbSymbolRoundedRectangle.Create(param);
				}
				else if (param.ToUpper().Contains("XC") is true)
				{
					// Chamfered Rectangle
					return OdbSymbolChamferedRectangle.Create(param);
				}
				else
				{
					// Rectangle
					return OdbSymbolRectangle.Create(param);
				}
			}
			else if (name.ToUpper().Equals("OVAL") is true)
			{
				// Oval
				return OdbSymbolOval.Create(param);
			}
			else if (name.ToUpper().Equals("DI") is true)
			{
				// Diaomond
				return OdbSymbolDiamond.Create(param);
			}
			else if (name.ToUpper().Equals("OCT") is true)
			{
				// Octagon
				return OdbSymbolOctagon.Create(param);
			}
			else if (name.ToUpper().Equals("DOUNT_R") is true)
			{
				// Round Donut
				return OdbSymbolRoundDonut.Create(param);
			}
			else if (name.ToUpper().Equals("DONUT_S") is true)
			{
				if (param.Count(x => x.Equals('x')) > 1)
				{
					// Square Donut
					return OdbSymbolSquareDonut.Create(param);
				}
				else
				{
					// Rounded Square Donut
					return OdbSymbolRoundedSqureDonut.Create(param);
				}
			}
			else if (name.ToUpper().Equals("DONUT_SR") is true)
			{
				// Square Round Donut
				return OdbSymbolSquareRoundDonut.Create(param);
			}
			else if (name.ToUpper().Equals("DONUT_RC") is true)
			{
				if (param.Count(x => x.Equals('x')) > 2)
				{
					// Rounded Rectangle Donut
					return OdbSymbolRoundedRectangleDonut.Create(param);
				}
				else
				{
					// Rectangle Donut
					return OdbSymbolRectangleDonut.Create(param);
				}
			}
			else if (name.ToUpper().Equals("DONUT_O") is true)
			{
				// Oval Donut
				return OdbSymbolOvalDonut.Create(param);
			}
			else if (name.ToUpper().Equals("HEX_L") is true)
			{
				// Horizontal Hexagon
				return OdbSymbolHorizontalHexagon.Create(param);
			}
			else if (name.ToUpper().Equals("HEX_S") is true)
			{
				// Vertical Hexagon
				return OdbSymbolVerticalHexagon.Create(param);
			}
			else if (name.ToUpper().Equals("BFR") is true)
			{
				// Butterfly
				return OdbSymbolButterfly.Create(param);
			}
			else if (name.ToUpper().Equals("BFS") is true)
			{
				// Square Butterfly
				return OdbSymbolSquareButterfly.Create(param);
			}
			else if (name.ToUpper().Equals("TRI") is true)
			{
				// Triangle
				return OdbSymbolTriangle.Create(param);
			}
			else if (name.ToUpper().Equals("OVAL_H") is true)
			{
				// Half Oval
				return OdbSymbolHalfOval.Create(param);
			}
			else if (name.ToUpper().Equals("THR") is true)
			{
				// Round Thermal (Rounded)
				return OdbSymbolRoundThermalRounded.Create(param);
			}
			else if (name.ToUpper().Equals("THS") is true)
			{
				// Round Thermal (Squared)
				return OdbSymbolRoundThermalSquared.Create(param);
			}
			else if (name.ToUpper().Equals("S_THS") is true)
			{
				// Square Thermal
				return OdbSymbolSquareThermal.Create(param);
			}
			else if (name.ToUpper().Equals("S_THO") is true)
			{
				// Square Thermal (Open Corners)
				return OdbSymbolSquareThermalOpenCorners.Create(param);
			}
			else if (name.ToUpper().Equals("SR_THS") is true)
			{
				// Square-Round Thermal
				return OdbSymbolSquareRoundThermal.Create(param);
			}
			else if (name.ToUpper().Equals("RC_THS") is true)
			{
				// Rectangular Thermal
				return OdbSymbolRectangularThermal.Create(param);
			}
			else if (name.ToUpper().Equals("RC_THO") is true)
			{
				// Rectangular Thermal (Open Corners)
				return OdbSymbolRectangularThermalOpenCorners.Create(param);
			}
			else if (name.ToUpper().Equals("S_THS") is true)
			{
				// Rounded Square Thermal
				return OdbSymbolRoundedSquareThermal.Create(param);
			}
			else if (name.ToUpper().Equals("S_THS") is true)
			{
				// Rounded Square Thermal (Open Corners)
				return OdbSymbolRoundedSquareThermalOpenCorners.Create(param);
			}
			else if (name.ToUpper().Equals("RC_THS") is true)
			{
				string[] value = param.Split('X');
				if (value[2].Equals("0") is true)
				{
					// Rounded Rectangle Thermal
					return OdbSymbolRoundedRectangleThermal.Create(param);
				}
				else if (value[2].Equals("45") is true)
				{
					// Rounded Rectangle Thermal(Open Corners)
					return OdbSymbolRoundedRectangleThermalOpenCorners.Create(param);
				}
			}
			else if (name.ToUpper().Equals("O_THS") is true)
			{
				string[] value = param.Split('X');
				if (value[2].Equals("0") is true)
				{
					// Oval Thermal 
					return OdbSymbolOvalThermal.Create(param);
				}
				else if (value[2].Equals("45") is true)
				{
					// Oval Thermal (Open Corners)
					return OdbSymbolOvalThermalOpenCorners.Create(param);
				}
			}
			else if (name.ToUpper().Equals("EL") is true)
			{
				// Ellipse
				return OdbSymbolEllipse.Create(param);
			}
			else if (name.ToUpper().Equals("MOIRE") is true)
			{
				// Moire
				return OdbSymbolMoire.Create(param);
			}
			else if (name.ToUpper().Equals("HOLE") is true)
			{
				// Hole
				return OdbSymbolHole.Create(param);
			}

			return null;
		}
	}
}
