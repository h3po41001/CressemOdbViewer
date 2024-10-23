using System.Collections.Generic;

namespace ImageControl.Shape.Interface
{
	public interface IShapeList
	{
		float Xdatum { get; }

		float Ydatum { get; }

		// 0 : 0도, 1 : 90도, 2 : 180도, 3 : 270도, 4 : 0도 X축 미러, 5 : 90도 X축 미러, 6 : 180도 X축 미러, 7 : 270도 X축 미러
		// 8 :  any angle rotation, no mirror, 9 : any angle rotation, X-axis mirror
		int OrientAngle { get; }

		bool IsMirrorXAxis { get; }

		IEnumerable<IShapeBase> Shapes { get; }
	}
}
