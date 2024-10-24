using System.Collections.Generic;
using ImageControl.Shape.Interface;

namespace CressemDataToGraphics.Model.Graphics.Shape
{
	internal class ShapeList : IShapeList
	{
		private readonly List<IShapeBase> _shapes;

		//private ShapeList() { }

		public ShapeList(/*double xDatum, double yDatum, int orient*/)
		{
			//Xdatum = (float)xDatum;
			//Ydatum = (float)yDatum;
			//Orient = orient;
			_shapes = new List<IShapeBase>();
		}

		//public float Xdatum { get; private set; }

		//public float Ydatum { get; private set; }

		//// 0 : 0도, 1 : 90도, 2 : 180도, 3 : 270도, 4 : 0도 X축 미러, 5 : 90도 X축 미러, 6 : 180도 X축 미러, 7 : 270도 X축 미러
		//// 8 : any angle rotation, no mirror, 9 : any angle rotation, X-axis mirror
		//public int Orient { get; private set; }

		//public int OrientAngle { get => (Orient % 4) * 90; }

		//public bool IsMirrorXAxis { get => Orient >= 4; }

		public IEnumerable<IShapeBase> Shapes { get => _shapes; }

		public void AddShape(IEnumerable<IShapeBase> shapes)
		{
			if (shapes != null)
			{
				_shapes.AddRange(shapes);
			}
		}
	}
}
