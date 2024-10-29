using System.Drawing;

namespace ImageControl.Model.Shape.Gdi
{
	internal abstract class GdiShape
	{
		protected GdiShape() { }

		protected GdiShape(bool isPositive)
		{
			IsPositive = isPositive;
			SolidBrush = new SolidBrush(Color.DarkGreen);
			DefaultPen = new Pen(Color.White, 0.1f);
			ProfilePen = new Pen(Color.White, 0.1f);
		}

		public bool IsPositive { get; private set; }

		protected SolidBrush SolidBrush { get; private set; }

		protected Pen DefaultPen { get; private set; }

		protected Pen ProfilePen { get; private set; }

		public abstract void Fill(Graphics graphics);

		public abstract void Draw(Graphics graphics);
	}
}
