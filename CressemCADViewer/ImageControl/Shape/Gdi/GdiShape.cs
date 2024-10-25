using System.Drawing;
using ImageControl.Shape.Gdi.Interface;

namespace ImageControl.Model.Shape.Gdi
{
	internal abstract class GdiShape : IGdiBase
	{
		protected GdiShape()
		{
			SolidBrush = new SolidBrush(Color.DarkGreen);
			DefaultPen = new Pen(Color.White, 0.1f);
			ProfilePen = new Pen(Color.White, 0.1f);
		}

		protected SolidBrush SolidBrush { get; private set; }

		protected Pen DefaultPen { get; private set; }

		protected Pen ProfilePen { get; private set; }

		public abstract void Fill(Graphics graphics);

		public abstract void Draw(Graphics graphics);
	}
}
