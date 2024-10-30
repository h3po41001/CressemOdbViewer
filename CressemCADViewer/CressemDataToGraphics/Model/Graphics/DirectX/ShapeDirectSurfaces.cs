using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageControl.Shape.DirectX.Interface;

namespace CressemDataToGraphics.Model.Graphics.DirectX
{
	internal class ShapeDirectSurfaces : ShapeDirectBase, IDirectSurfaces
	{
		private List<ShapeDirectSurface> _surfaces;

		private ShapeDirectSurfaces() : base() { }

		public ShapeDirectSurfaces(IEnumerable<ShapeDirectSurface> surfaces) : base()
		{
			_surfaces = new List<ShapeDirectSurface>(surfaces);
		}

		public IEnumerable<IDirectSurface> Surfaces { get => _surfaces; }
	}
}
