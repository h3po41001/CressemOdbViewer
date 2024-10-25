using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CressemExtractLibrary.Data.Interface.Symbol;
using ImageControl.Shape.Gdi.Interface;

namespace CressemDataToGraphics.Factory
{
	internal class OpenGlFactory
	{
		public OpenGlFactory()
		{
		}

		public IGdiBase CreateFeatureToShape()
		{
			throw new NotImplementedException();
		}

		public IGdiBase CreateSymbolToShape()
		{
			throw new NotImplementedException();
		}
	}
}
