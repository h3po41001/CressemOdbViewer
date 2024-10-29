namespace CressemDataToGraphics.Model.Cad
{
	internal class ShapeBase
	{
		protected ShapeBase() { }

		protected ShapeBase(float cx, float cy)
		{
			ShapeCx = cx;
			ShapeCy = cy;
		}

		public float ShapeCx { get; private set; }

		public float ShapeCy { get; private set; }
	}
}
